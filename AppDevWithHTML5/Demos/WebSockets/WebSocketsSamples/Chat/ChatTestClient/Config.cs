// <copyright file="Config.cs" company="Microsoft Corporation">
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.ServiceModel.WebSockets.Samples
{
    using System;

    /// <summary>
    /// WebSocket protocol version.
    /// </summary>
    public enum WebSocketVersion
    {
        /// <summary>
        /// Use a random version number
        /// </summary>
        Random = 0,     // Random must be first entry (0)

        /// <summary>
        /// Use WebSocket protocol version 75
        /// </summary>
        v75,

        /// <summary>
        /// Use WebSocket protocol version 76
        /// </summary>
        v76,

        /// <summary>
        /// Use WebSocket protocol version 03
        /// </summary>
        v03,
    }

    /// <summary>
    /// Chat test client configuration
    /// </summary>
    internal class Config
    {
        private Random random = new Random(0);

        /// <summary>
        /// Initializes a new instance of the <see cref="Config"/> class.
        /// </summary>
        public Config()
        {
            this.HostPort = Environment.MachineName + ":4502";
            this.Clients = 1;
            this.Version = WebSocketVersion.v03;
            this.Quiet = false;
            this.LoopCount = 1;
            this.SteadystateOnly = true;
            this.SteadystateDelay = 1000;
            this.SteadystateCount = 100;
            this.ClientStartDelay = 125;
        }

        /// <summary>
        /// Gets the hostname and port of Websocket URI to connect to. 
        /// </summary>
        public string HostPort { get; private set; }

        /// <summary>
        /// Gets the number of simultaneous clients to start talking to the service endpoint. 
        /// </summary>
        public int Clients { get; private set; }

        /// <summary>
        /// Gets the WebSocket version that clients should use. 
        /// </summary>
        public WebSocketVersion Version { get; private set; }

        /// <summary>
        /// Gets a value indicating whether to output verbose trace messages.
        /// </summary>
        public bool Quiet { get; private set; }

        /// <summary>
        /// Gets the number of rotating message loops.
        /// </summary>
        public int LoopCount { get; private set; }

        /// <summary>
        /// Gets a value indicating whether to jump directly to steady state. 
        /// </summary>
        public bool SteadystateOnly { get; private set; }

        /// <summary>
        /// Gets the delay (in ms) used between messages in steady state. 
        /// </summary>
        public int SteadystateDelay { get; private set; }

        /// <summary>
        /// Gets the number of steady state messages. 
        /// </summary>
        public int SteadystateCount { get; private set; }

        /// <summary>
        /// Gets the delay (in ms) between starting each client.
        /// </summary>
        public int ClientStartDelay { get; private set; }

        /// <summary>
        /// Gets the client version specified in config or a random version
        /// number.
        /// </summary>
        /// <returns>WebSocket version.</returns>
        public WebSocketVersion GetClientVersion()
        {
            if (Program.Config.Version == WebSocketVersion.Random)
            {
                var count = Enum.GetNames(typeof(WebSocketVersion)).Length;
                return (WebSocketVersion)this.random.Next(1, count);
            }
            else
            {
                return Program.Config.Version;
            }
        }
    }
}