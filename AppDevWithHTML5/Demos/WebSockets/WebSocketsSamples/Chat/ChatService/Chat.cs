// <copyright file="Chat.cs" company="Microsoft Corporation">
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.ServiceModel.WebSockets.Samples
{
    using System;
    using System.ServiceModel;
    using System.Threading;

    /// <summary>
    /// Chat service implementation.
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ChatService : WebSocketsService
    {
        private static ChatSessions sessions = new ChatSessions();
        private static int globalId;

        private int id;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatService"/> class.
        /// </summary>
        public ChatService()
        {
            this.id = Interlocked.Increment(ref ChatService.globalId);
            if (!ChatService.sessions.TryAdd(this))
            {
                throw new InvalidOperationException("Can't add session.");
            }
        }

        public override void OnMessage(string value)
        {
            ChatService.sessions.RelayMessage(this, value);
        }

        /// <summary>
        /// Returns the hash code for this instance
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return this.id;
        }

        protected override void OnClose(object sender, EventArgs e)
        {
            ChatService.sessions.Remove(this);
        }
    }
}
