// <copyright file="Program.cs" company="Microsoft Corporation">
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.ServiceModel.WebSockets.Samples
{
    using System;
    using System.Configuration;
    using System.Threading;

    /// <summary>
    /// WCF Game Service using WebSockets.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Initializes the gameService.
        /// </summary>
        private static void Main()
        {
            string appName = ConfigurationManager.AppSettings["AppName"];
            string portNumber = ConfigurationManager.AppSettings["PortNumber"];
            string machineName = ConfigurationManager.AppSettings["MachineName"];
            string uriString = machineName + ":" + portNumber + "/" + appName;

            var sh = new WebSocketsHost<GameService>(new Uri("ws://" + uriString));
            sh.AddWebSocketsEndpoint();
            sh.Open();

            Console.WriteLine("Websocket game server listening on " + sh.Description.Endpoints[0].Address.Uri.AbsoluteUri);
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("NOTE:");
            Console.WriteLine("1. Validate http://localhost/clientaccesspolicy.xml is accessible from the browser before running the game client.");
            Console.WriteLine("   (See detailed instructions in clientaccesspolicy.xml in the GameService project.)");
            Console.WriteLine("2. Ensure the firewall allows incoming TCP traffic on port 4502 before running the game client.");

            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine("Press Ctrl-C to terminate the game server...");

            using (ManualResetEvent manualResetEvent = new ManualResetEvent(false))
            {
                manualResetEvent.WaitOne();
            }

            sh.Close();
        }
    }
}