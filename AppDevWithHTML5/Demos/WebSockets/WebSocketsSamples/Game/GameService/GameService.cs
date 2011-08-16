// <copyright file="GameService.cs" company="Microsoft Corporation">
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.ServiceModel.WebSockets.Samples
{
    using System;
    using System.ServiceModel;
    using System.Threading;

    /// <summary>
    /// Game service implementation.
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class GameService : WebSocketsService
    {
        /// <summary>
        /// Reference to all game service instances.
        /// </summary>
        private static GameSessions sessions = new GameSessions();

        /// <summary>
        /// Assigns identifier to a session.
        /// </summary>
        private static int globalId;

        /// <summary>
        /// Numebr of messages. Just for debugging purposes.
        /// </summary>
        private static int noOfMessages = 0;

        /// <summary>
        /// Session idetifier.
        /// </summary>
        private int id;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameService"/> class.
        /// </summary>
        public GameService()
        {
            this.id = Interlocked.Increment(ref GameService.globalId);
            if (!GameService.sessions.TryAdd(this))
            {
                throw new InvalidOperationException("Can't add session.");
            }

            this.Context = new GameServiceContext { LogOnName = null, BuddyInstance = null };
        }

        /// <summary>
        /// Gets the context of a session.
        /// </summary>
        public GameServiceContext Context { get; private set; }

        /// <summary>
        /// Gets invoked whenever a message arrives to this session.
        /// </summary>
        /// <param name="value">Message sent by GameClient</param>
        public override void OnMessage(string value)
        {
            Interlocked.Increment(ref GameService.noOfMessages);
            if (null != value)
            {
                Logger.LogMessage("Message arrived at Service:" + value);
                GameCommandHandler.HandleGameMessage(this, GameService.sessions, value);
            }
        }

        /// <summary>
        /// Returns the hash code for this instance
        /// </summary>
        /// <returns>32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return this.id;
        }

        /// <summary>
        /// Gets invoked whenever this session closes.
        /// </summary>
        /// <param name="sender">Sender of close event.</param>
        /// <param name="e">Event args.</param>
        protected override void OnClose(object sender, EventArgs e)
        {
            if (null != this.Context && !string.IsNullOrEmpty(this.Context.LogOnName))
            {
                Command.Login.RemoveUser();
            }

            GameService.sessions.Remove(this);
        }
    }
}
