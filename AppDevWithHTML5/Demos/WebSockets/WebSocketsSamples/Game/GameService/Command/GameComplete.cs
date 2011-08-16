// <copyright file="GameComplete.cs" company="Microsoft Corporation">
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.ServiceModel.WebSockets.Samples.Command
{
    /// <summary>
    /// Defines handler for GameComplete command.
    /// </summary>
    internal class GameComplete : IGameCommand
    {
        /// <summary>
        /// Lock object for this class.
        /// </summary>
        private static object gameCompleteLock = new object();

        /// <summary>
        /// Gets the name of the command.
        /// </summary>
        public string Name
        {
            get { return "GameComplete"; }
        }

        /// <summary>
        /// Implements execution of this command.
        /// </summary>
        /// <param name="current">Current gameservice instance.</param>
        /// <param name="sessions">Collection of all sessons.</param>
        /// <param name="message">Command parameters.</param>
        public void Execute(GameService current, GameSessions sessions, string message)
        {
            if (null == current.Context.BuddyInstance)
            {
                sessions.SendMessage(current, "GameCompleteResponse:Failure;Buddy does not exist.");
                return;
            }
            else
            {
                GameService buddyInstance = current.Context.BuddyInstance;
                lock (gameCompleteLock)
                {
                    if (null != buddyInstance)
                    {
                        buddyInstance.Context.BuddyInstance = null;
                    }

                    if (null != current)
                    {
                        current.Context.BuddyInstance = null;
                    }

                    // Need to send gameComplete response to the current service, gameComplete to buddyInstance
                    // and also we need to braodcast gameCompleteResponse to all other sessions.
                    sessions.BroadcastMessage(BroadcastMessageType.GameCompleteResponse, current, buddyInstance, "GameCompleteResponse:Successful", "GameComplete");
                    return;
                }
            }
        }
    }
}
