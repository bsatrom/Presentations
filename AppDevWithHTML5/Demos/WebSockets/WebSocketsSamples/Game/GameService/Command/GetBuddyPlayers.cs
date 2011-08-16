// <copyright file="GetBuddyPlayers.cs" company="Microsoft Corporation">
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.ServiceModel.WebSockets.Samples.Command
{
    /// <summary>
    /// Handles the GetBuddyPlayers Command.
    /// </summary>
    internal class GetBuddyPlayers : IGameCommand
    {
        /// <summary>
        /// Gets the command string..
        /// </summary>
        public string Name
        {
            get { return "GetBuddyPlayers"; }
        }

        /// <summary>
        /// Implements the execution of getbuddyPlayer command.
        /// </summary>
        /// <param name="current">Current service instance.</param>
        /// <param name="sessions">Collection of all sessions.</param>
        /// <param name="message">Command arguments.</param>
        public void Execute(GameService current, GameSessions sessions, string message)
        {
            var players = string.Empty;
            players = sessions.GetOtherLoggedInSessionsList(current);
            
            // Removes last semicolon from players string.
            if (!string.IsNullOrEmpty(players))
            {
                players = players.Substring(0, players.Length - 1);
            }

            sessions.SendMessage(current, "GetBuddyPlayersResponse:" + players);
        }
    }
}
