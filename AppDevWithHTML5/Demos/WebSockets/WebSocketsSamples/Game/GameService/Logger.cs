// <copyright file="Logger.cs" company="Microsoft Corporation">
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.ServiceModel.WebSockets.Samples
{
    using System;
    using System.Configuration;

    /// <summary>
    /// Logs the messages.
    /// </summary>
    internal class Logger
    {
        /// <summary>
        /// Logging mode.
        /// </summary>
        private static string logMode = ConfigurationManager.AppSettings["LogMessage"];

        /// <summary>
        ///  Prevents a default instance of the Logger class from being created.
        /// </summary>
        private Logger()
        {
        }

        /// <summary>
        /// Logs messages to the console output.
        /// </summary>
        /// <param name="message">Message string.</param>
        public static void LogMessage(string message)
        {
            if (logMode != null && logMode.Equals("true"))
            {
                Console.WriteLine(message);
            }
        }
    }
}