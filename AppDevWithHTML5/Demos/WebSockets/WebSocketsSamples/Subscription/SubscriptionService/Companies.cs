// <copyright file="Companies.cs" company="Microsoft Corporation">
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.ServiceModel.WebSockets.Samples
{
    using System;
    using System.Threading;

    /// <summary>
    /// Collection used to manage WebSocket Chat service instances.
    /// </summary>
    internal class Companies : IDisposable
    {
        private Random random = new Random();
        private bool disposed = false;
        private ReaderWriterLockSlim thisLock = new ReaderWriterLockSlim();
        private CompanyInfoCollection innerCache;

        public Companies(params CompanyInfo[] companies)
        {
            this.innerCache = new CompanyInfoCollection();
            foreach (var item in companies)
            {
                this.innerCache.Add(item);
            }
        }

        public void UpdateValues()
        {
            this.thisLock.EnterWriteLock();
            try
            {
                double mult = (105 - this.random.Next(0, 10)) / 100.0;
                foreach (var item in this.innerCache)
                {
                    item.StockPrice *= mult;
                }
            }
            finally
            {
                this.thisLock.ExitWriteLock();
            }
        }

        public bool TryGetValue(string symbol, out CompanyInfo value)
        {
            this.thisLock.EnterReadLock();
            try
            {
                if (this.innerCache.Contains(symbol))
                {
                    value = this.innerCache[symbol];
                    return true;
                }

                value = null;
                return false;
            }
            finally
            {
                this.thisLock.ExitReadLock();
            }
        }

        public void Dispose()
        {
            if (!this.disposed)
            {
                if (this.thisLock != null)
                {
                    this.thisLock.Dispose();
                }

                this.disposed = true;
            }
        }
    }
}