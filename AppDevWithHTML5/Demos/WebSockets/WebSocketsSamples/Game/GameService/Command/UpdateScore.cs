// <copyright file="UpdateScore.cs" company="Microsoft Corporation">
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.ServiceModel.WebSockets.Samples.Command
{
    /// <summary>
    /// Defines the handler of updateScore command.
    /// </summary>
    internal class UpdateScore : IGameCommand
    {
        /// <summary>
        /// Gets the name of updatescore command.
        /// </summary>
        public string Name
        {
            get { return "UpdateScore"; }
        }

        /// <summary>
        /// Implements the execution of the updateScore command.
        /// </summary>
        /// <param name="current">Current service instance.</param>
        /// <param name="sessions">Collection of all sessions.</param>
        /// <param name="message">Command arguments.</param>
        public void Execute(GameService current, GameSessions sessions, string message)
        {
            if (null == current.Context.BuddyInstance)
            {
                sessions.SendMessage(current, "UpdateScoreResponse:Failure;Buddy not found.");
                return;
            }
            else
            {
                if (string.IsNullOrEmpty(message))
                {
                    sessions.SendMessage(current, "UpdateScoreResponse:Failure;Score is not found.");
                    return;
                }

                 sessions.SendMessage(current.Context.BuddyInstance, "FixBuddyScore:" + message);
                 sessions.SendMessage(current, "UpdateScoreResponse:Successful");
            }
        }
    }
}
