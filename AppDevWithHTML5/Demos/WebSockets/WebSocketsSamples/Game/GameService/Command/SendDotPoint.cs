// <copyright file="SendDotPoint.cs" company="Microsoft Corporation">
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.ServiceModel.WebSockets.Samples.Command
{
    using System;

    /// <summary>
    /// Defines the handler for the sendDotPoint command.
    /// </summary>
    internal class SendDotPoint : IGameCommand
    {
        /// <summary>
        /// Gets the name of sendDotPoint command.
        /// </summary>
        public string Name
        {
            get { return "SendDotPoint"; }
        }

        /// <summary>
        /// Implements the execution of the sendDotPoint command.
        /// </summary>
        /// <param name="current">Current service instance.</param>
        /// <param name="sessions">Collection of all sessions.</param>
        /// <param name="message">Command arguments.</param>
        public void Execute(GameService current, GameSessions sessions, string message)
        {
            if (null == current.Context.BuddyInstance)
            {
                sessions.SendMessage(current, "SendDotPointResponse:Failure;BuddyNotFound.");
                return;
            }
            else
            {
                if (string.IsNullOrEmpty(message))
                {
                    sessions.SendMessage(current, "SendDotPointResponse:Failure;WrongFormat.");
                    return;
                }

                string[] location = message.Split(new char[] { ';' });

                if (location.Length != 2)
                {
                    sessions.SendMessage(current, "SendDotPointResponse:Failure;WrongFormat.");
                    return;
                }

                sessions.SendMessage(current.Context.BuddyInstance, "FixDotPoint:" + location[0] + ";" + location[1]);
                sessions.SendMessage(current, "SendDotPointResponse:Successful");
            }
        }
    }
}
