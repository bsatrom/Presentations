// <copyright file="Login.cs" company="Microsoft Corporation">
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.ServiceModel.WebSockets.Samples.Command
{
    using System.Threading;

    /// <summary>
    /// Defines the handler for the login command.
    /// </summary>
    internal class Login : IGameCommand
    {
        /// <summary>
        ///  Maximum number of allowed users.
        /// </summary>
        private const int MaxUsers = 10;

        /// <summary>
        /// Total number of logged in users.
        /// </summary>
        private static int loggedInUsers = 0;

        /// <summary>
        /// Gets the name of the command.
        /// </summary>
        public string Name
        {
            get { return "Login"; }
        }

        /// <summary>
        /// Gets the number of the logged in users.
        /// </summary>
        public static void RemoveUser()
        {
            Interlocked.Decrement(ref loggedInUsers);
        }

        /// <summary>
        /// Implements the execution of login command.
        /// </summary>
        /// <param name="current">Current service instance.</param>
        /// <param name="sessions">Collection of all sessions.</param>
        /// <param name="message">Command arguments.</param>
        public void Execute(GameService current, GameSessions sessions, string message)
        {
            if (!string.IsNullOrEmpty(current.Context.LogOnName))
            {
                sessions.SendMessage(current, "LoginResponse:Failure;Loggedin"); 
                return;
            }

            if (string.IsNullOrEmpty(message))
            {
                sessions.SendMessage(current, "LoginResponse:Failure;LoginNameEmpty");
                return;
            }

            var service = sessions.FindSession(message);

            if (null == service && loggedInUsers < MaxUsers)
            {
                Interlocked.Increment(ref loggedInUsers);
                current.Context.LogOnName = message;

                // Need to send successful response to the current service and LogOn response to all other sessions.
                // so that other sessions can also see that this user is logged in.
                sessions.BroadcastMessage(BroadcastMessageType.LogOnResponse, current, null, "LoginResponse:Successful", null);
            }
            else if (MaxUsers == loggedInUsers)
            {
                sessions.SendMessage(current, "LoginResponse:Failure;LoginLimitReached");
            }
            else
            {
                sessions.SendMessage(current, "LoginResponse:Failure;LoginNameTaken");
            }
        }
    }
}
