// <copyright file="SelectBuddy.cs" company="Microsoft Corporation">
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.ServiceModel.WebSockets.Samples.Command
{
    /// <summary>
    /// Defines the handler for the selectBuddy command. 
    /// </summary>
    internal class SelectBuddy : IGameCommand
    {
        /// <summary>
        /// selectBuddy object lock.
        /// </summary>
        private static object selectBuddyLock = new object();

        /// <summary>
        /// Gets the name of selectBuddy command.
        /// </summary>
        public string Name
        {
            get { return "SelectBuddy"; }
        }

        /// <summary>
        /// Implements the execution of selectBuddy command.
        /// </summary>
        /// <param name="current">Current service instance.</param>
        /// <param name="sessions">Collection of all sessions.</param>
        /// <param name="message">Command arguments.</param>
        public void Execute(GameService current, GameSessions sessions, string message)
        {
            if (null != current.Context.BuddyInstance)
            {
                sessions.SendMessage(current, "SelectBuddyResponse:Failure;BuddyExist.");
                return;
            }
            else
            {
                if (string.IsNullOrEmpty(message))
                {
                    sessions.SendMessage(current, "SelectBuddyResponse:Failure;WrongRequest.");
                    return;
                }
                
                var buddyInstance = sessions.FindSession(message);
                
                if (null == buddyInstance)
                {
                    sessions.SendMessage(current, "SelectBuddyResponse:Failure;BuddyDoesNotExist" + message);
                    return;
                }
                else
                {
                    // Associate both players and then send all responses in the same atomic operation.
                    lock (selectBuddyLock)
                    {
                        if (null == buddyInstance.Context.BuddyInstance && null == current.Context.BuddyInstance)
                        {
                            current.Context.BuddyInstance = buddyInstance;
                            buddyInstance.Context.BuddyInstance = current;

                            // Need to send selectBuddy response to the current service, fixedbuddyresponse to buddyInstance
                            // and also we need to braodcast selectBuddyResposne to other sessions.
                            sessions.BroadcastMessage(BroadcastMessageType.SelectBuddyResponse, current, buddyInstance, "SelectBuddyResponse:Successful", "FixedBuddyResponse:" + current.Context.LogOnName);
                            return;
                        }  
                    }
                    
                    // Selected player is already a buddy.
                    sessions.SendMessage(current, "SelectBuddyResponse:Failure;BusyBuddy;" + message);
                }
            }
        }
    }
}
