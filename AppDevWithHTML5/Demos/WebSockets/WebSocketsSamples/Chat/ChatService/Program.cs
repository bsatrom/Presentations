// <copyright file="Program.cs" company="Microsoft Corporation">
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.ServiceModel.WebSockets.Samples
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Description;
    using System.Threading;

    /// <summary>
    /// WCF Chat Service using WebSockets
    /// </summary>
    internal class Program
    {
        private const int Instances = 512;

        private static void Main()
        {
            var sh = new WebSocketsHost<ChatService>(new Uri("ws://" + Environment.MachineName + ":4502/chat"));
            sh.AddWebSocketsEndpoint();
            sh.Open();

            Console.WriteLine("Websocket chat server listening on " + sh.Description.Endpoints[0].Address.Uri.AbsoluteUri);
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("NOTE:");
            Console.WriteLine("1. Validate http://localhost/clientaccesspolicy.xml is accessible from the browser before running the chat client.");
            Console.WriteLine("   (See detailed instructions in clientaccesspolicy.xml in the ChatService project.)");
            Console.WriteLine("2. Ensure the firewall allows incoming TCP traffic on port 4502 before running the chat client.");

            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine("Press Ctrl-C to terminate the chat server...");

            using (ManualResetEvent manualResetEvent = new ManualResetEvent(false))
            {
                manualResetEvent.WaitOne();
            }

            sh.Close();
        }
    }
}
