// <copyright file="IGameCommand.cs" company="Microsoft Corporation">
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.ServiceModel.WebSockets.Samples.Command
{
    using Microsoft.ServiceModel.WebSockets.Samples;

    /// <summary>
    /// Defines the interface for a command sent by the GameClient.
    /// </summary>
    internal interface IGameCommand
    {
        /// <summary>
        /// Gets the name of the command.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Implements the execution of the command.
        /// </summary>
        /// <param name="current">Current service instance.</param>
        /// <param name="sessions">Collection of all sessions.</param>
        /// <param name="message">Command parameters.</param>
        void Execute(GameService current, GameSessions sessions, string message);
    }
}
