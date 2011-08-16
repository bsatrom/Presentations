// <copyright file="BroadcastMessageType.cs" company="Microsoft Corporation">
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.ServiceModel.WebSockets.Samples
{
    /// <summary>
    /// Broadcast message types.
    /// </summary>
    public enum BroadcastMessageType
    {
        /// <summary>
        /// Log in response type.
        /// </summary>
        LogOnResponse = 0,     // Broadcast in response to login request. 

        /// <summary>
        /// Select buddy response type.
        /// </summary>
        SelectBuddyResponse,  // Broadcast in response to select buddy request.

        /// <summary>
        /// Game complete response type.
        /// </summary>
        GameCompleteResponse, // Broadcast in response to game complete request.

        /// <summary>
        /// Log out response type.
        /// </summary>
        LogOffResponse, // Broadcast in response to log out reuest.
    }
}
