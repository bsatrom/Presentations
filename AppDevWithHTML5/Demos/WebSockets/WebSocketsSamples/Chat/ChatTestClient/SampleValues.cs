// <copyright file="SampleValues.cs" company="Microsoft Corporation">
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.ServiceModel.WebSockets.Samples
{
    using System;

    /// <summary>
    /// Utility class for handling sample values.
    /// </summary>
    internal static class SampleValues
    {
        private static Random random = new Random();
        private static object thisLock = new object();

        /// <summary>
        /// Gets a value randomly distributed between mean +- deviation.
        /// </summary>
        /// <param name="mean">The mean value.</param>
        /// <param name="deviation">The max deviation from the mean in positive and negative direction.</param>
        /// <returns>The next sample value.</returns>
        public static int GetNextSample(int mean, int deviation)
        {
            lock (SampleValues.thisLock)
            {
                var result = mean + SampleValues.random.Next(0, (2 * deviation) + 1) - deviation;
                return result;
            }
        }
    }
}
