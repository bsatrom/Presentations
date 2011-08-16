// <copyright file="ChatSessions.cs" company="Microsoft Corporation">
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.ServiceModel.WebSockets.Samples
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// Collection used to manage WebSocket Chat service instances.
    /// </summary>
    internal class ChatSessions : IDisposable
    {
        private ServiceCollection<ChatService> innerCache = new ServiceCollection<ChatService>();
        private ReaderWriterLockSlim thisLock = new ReaderWriterLockSlim();

        /// <summary>
        /// Relaying the contents from one session to all other sessions.
        /// </summary>
        /// <param name="current">The current session relaying the content.</param>
        /// <param name="value">Content to relay.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "current", Justification = "Will be used later.")]
        public void RelayMessage(ChatService current, string value)
        {
            List<ChatService> defunct = null;
            this.thisLock.EnterReadLock();
            try
            {
                foreach (var entry in this.innerCache)
                {
                    try
                    {
                        entry.SendMessage(value);
                    }
                    catch
                    {
                        if (defunct == null)
                        {
                            defunct = new List<ChatService>();
                        }

                        defunct.Add(entry);
                    }
                }
            }
            finally
            {
                this.thisLock.ExitReadLock();
            }

            if (defunct != null)
            {
                this.thisLock.EnterWriteLock();
                try
                {
                    foreach (var entry in defunct)
                    {
                        this.innerCache.Remove(entry);
                    }
                }
                finally
                {
                    this.thisLock.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// Attempting to add another session to the collection.
        /// </summary>
        /// <param name="entry">Session to add.</param>
        /// <returns>true if session was added; false otherwise.</returns>
        public bool TryAdd(ChatService entry)
        {
            this.thisLock.EnterUpgradeableReadLock();
            try
            {
                if (this.innerCache.Contains(entry))
                {
                    return false;
                }

                this.thisLock.EnterWriteLock();
                try
                {
                    this.innerCache.Add(entry);
                    return true;
                }
                finally
                {
                    this.thisLock.ExitWriteLock();
                }
            }
            finally
            {
                this.thisLock.ExitUpgradeableReadLock();
            }
        }

        /// <summary>
        /// Attempting to remove a session from the collection.
        /// </summary>
        /// <param name="entry">Session to remove.</param>
        public void Remove(ChatService entry)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(this.RemoveInternal), entry);
        }

        /// <summary>
        /// Disposes the instance.
        /// </summary>
        public void Dispose()
        {
            this.thisLock.Dispose();
        }

        private void RemoveInternal(object state)
        {
            var entry = state as ChatService;
            if (entry != null)
            {
                this.thisLock.EnterWriteLock();
                try
                {
                    this.innerCache.Remove(entry);
                }
                finally
                {
                    this.thisLock.ExitWriteLock();
                }
            }
        }
    }
}