// <copyright file="SendMousePoint.cs" company="Microsoft Corporation">
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.ServiceModel.WebSockets.Samples.Command
{
    using System;

    /// <summary>
    /// Defines the handler for the sendMousePoint command.
    /// </summary>
    internal class SendMousePoint : IGameCommand
    {
        /// <summary>
        /// Gets the name of the sendMouseoint command.
        /// </summary>
        public string Name
        {
            get { return "SendMousePoint"; }
        }

        /// <summary>
        /// Implements the execution of the sendMousePoint command.
        /// </summary>
        /// <param name="current">Current service instance.</param>
        /// <param name="sessions">Collection of all sessions.</param>
        /// <param name="message">Command arguments.</param>
        public void Execute(GameService current, GameSessions sessions, string message)
        {
            if (null == current.Context.BuddyInstance)
            {
                sessions.SendMessage(current, "SendMousePointResponse:Failure;Buddy not found.");
                return;
            }
            else
            {
                if (string.IsNullOrEmpty(message))
                {
                    sessions.SendMessage(current, "SendMousePointResponse:Failure;Co-ordinates are not found in the request.");
                    return;
                }

                string[] location = message.Split(new char[] { ';' });

                if (location.Length != 2)
                {
                    sessions.SendMessage(current, "SendMousePointResponse:Failure;Co-ordinates are in incorrect format.");
                    return;
                }

                sessions.SendMessage(current.Context.BuddyInstance, "FixMousePoint:" + location[0] + ";" + location[1]);
                sessions.SendMessage(current, "SendMousePointResponse:Successful");
            }
        }
    }
}
