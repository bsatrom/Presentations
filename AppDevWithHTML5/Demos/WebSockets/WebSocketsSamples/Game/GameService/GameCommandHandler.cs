// <copyright file="GameCommandHandler.cs" company="Microsoft Corporation">
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.ServiceModel.WebSockets.Samples
{
    using System;
    using System.Collections.Generic;
    using Microsoft.ServiceModel.WebSockets.Samples.Command;

    /// <summary>
    /// Maps commands to their handlers.
    /// </summary>
    public sealed class GameCommandHandler
    {
        /// <summary>
        /// Dictionary maps the command names to the corresponding command handler.
        /// </summary>
        private static readonly Dictionary<string, IGameCommand> commands = InitializeList();

        /// <summary>
        ///  Prevents a default instance of the GameCommandHandler class from being created.
        /// </summary>
        private GameCommandHandler()
        {
        }

        /// <summary>
        /// Entry point for all commands execution.
        /// </summary>
        /// <param name="current"> Current session.</param>
        /// <param name="sessions">Collection of all sessions.</param>
        /// <param name="value">Message sent by chat cient.</param>
        internal static void HandleGameMessage(GameService current, GameSessions sessions, string value)
        {
            string command = string.Empty;
            string commandArgs = string.Empty;

            var message = value;
            var index = message.IndexOf(":", StringComparison.OrdinalIgnoreCase);
            if (index > 0)
            {
                command = message.Substring(0, index);
                commandArgs = message.Substring(index + 1, message.Length - index - 1);
            }
            else
            {
                command = message;
            }

            Logger.LogMessage(command);
            Logger.LogMessage(commandArgs);
        
            IGameCommand handler = null;
            if (commands.TryGetValue(command, out handler))
            {
                handler.Execute(current, sessions, commandArgs);
            }
        }

        /// <summary>
        /// Initializes the dictionary object.
        /// </summary>
        /// <returns>Commands dictionary.</returns>
        private static Dictionary<string, IGameCommand> InitializeList()
        {
            Dictionary<string, IGameCommand> list = new Dictionary<string, IGameCommand>();

            IGameCommand loginCommand = new Login();
            list.Add(loginCommand.Name, loginCommand);

            IGameCommand getBuddyPlayersCommand = new GetBuddyPlayers();
            list.Add(getBuddyPlayersCommand.Name, getBuddyPlayersCommand);

            IGameCommand selectBuddyCommand = new SelectBuddy();
            list.Add(selectBuddyCommand.Name, selectBuddyCommand);

            IGameCommand uiLoadCompleteCommand = new UILoadComplete();
            list.Add(uiLoadCompleteCommand.Name, uiLoadCompleteCommand);

            IGameCommand sendDotPointCommand = new SendDotPoint();
            list.Add(sendDotPointCommand.Name, sendDotPointCommand);

            IGameCommand sendMousePointCommand = new SendMousePoint();
            list.Add(sendMousePointCommand.Name, sendMousePointCommand);

            IGameCommand updateScoreCommand = new UpdateScore();
            list.Add(updateScoreCommand.Name, updateScoreCommand);

            IGameCommand gameCompleteCommand = new GameComplete();
            list.Add(gameCompleteCommand.Name, gameCompleteCommand);

            return list;
        }
    }
}