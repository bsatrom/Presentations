// <copyright file="GameServiceContext.cs" company="Microsoft Corporation">
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.ServiceModel.WebSockets.Samples
{
    /// <summary>
    /// Defines the conext of each service session.
    /// </summary>
    public class GameServiceContext
    {
        /// <summary>
        /// Gets or sets the login name of a session.
        /// </summary>
        public string LogOnName { get; set; }

        /// <summary>
        /// Gets or sets the buddyInstance of a session.
        /// </summary>
        public GameService BuddyInstance { get; set; }
    }
}