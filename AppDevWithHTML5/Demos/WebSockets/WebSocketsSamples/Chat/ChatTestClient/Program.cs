// <copyright file="Program.cs" company="Microsoft Corporation">
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.ServiceModel.WebSockets.Samples
{
    using System;
    using System.Globalization;
    using System.Threading;

    /// <summary>
    /// Simple test client for chat service.
    /// </summary>
    internal class Program
    {
        private static Config config = new Config();
        private static ClientCollection clients;
        private static Uri address;
        private static Timer timer;

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        public static Config Config
        {
            get
            {
                return Program.config;
            }
        }

        /// <summary>
        /// Remove client from client collection.
        /// </summary>
        /// <param name="client">Client to remove.</param>
        public static void RemoveClient(Client client)
        {
            if (Program.clients.Remove(client))
            {
                client.Dispose();
            }
        }

        /// <summary>
        /// Log verbose message to console.
        /// </summary>
        /// <param name="format">The formatting string.</param>
        /// <param name="parameters">The object array to write into format string.</param>
        public static void LogVerbose(string format, params object[] parameters)
        {
            if (!Program.config.Quiet)
            {
                Console.WriteLine(format, parameters);
            }
        }

        /// <summary>
        /// Log error message to error console.
        /// </summary>
        /// <param name="format">The formatting string.</param>
        /// <param name="parameters">The object array to write into format string.</param>
        public static void LogError(string format, params object[] parameters)
        {
            Console.Error.WriteLine(format, parameters);
        }

        private static void Main()
        {
            Program.address = new Uri(string.Format(CultureInfo.InvariantCulture, "ws://{0}/chat", Program.config.HostPort));
            Program.clients = new ClientCollection(Program.config.Clients);

            // Start clients with delay in between.
            Program.timer = new Timer(Program.AddClient, null, 0, Program.config.ClientStartDelay);

            Console.WriteLine("\nPress <ENTER> to terminate...");
            Console.ReadLine();
        }

        private static void AddClient(object context)
        {
            if (!Program.clients.IsFull)
            {
                var client = new Client(Program.address);
                Program.clients.Add(client);
                client.Communicate();
            }
        }
    }
}

