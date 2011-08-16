// <copyright file="Echo.cs" company="Microsoft Corporation">
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.ServiceModel.WebSockets.Samples
{
    using System.ServiceModel;

    /// <summary>
    /// Echo service implementation.
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class EchoService : WebSocketsService
    {
        /// <summary>
        /// Echo incoming message back
        /// </summary>
        /// <param name="value">The message received over WebSockets.</param>
        public override void OnMessage(string value)
        {
            this.SendMessage(value);
        }
    }
}