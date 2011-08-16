// <copyright file="Subscription.cs" company="Microsoft Corporation">
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.ServiceModel.WebSockets.Samples
{
    using System;
    using System.Globalization;
    using System.ServiceModel;
    using System.Threading;

    /// <summary>
    /// Subscription service implementation.
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class SubscriptionService : WebSocketsService
    {
        private bool disposed = false;
        private string[] subscription;
        private Timer timer = null;

        private Companies symbols = new Companies(
            new CompanyInfo { StockSymbol = "msft", StockPrice = 25 },
            new CompanyInfo { StockSymbol = "appl", StockPrice = 90 },
            new CompanyInfo { StockSymbol = "ibm", StockPrice = 130 },
            new CompanyInfo { StockSymbol = "orcl", StockPrice = 26 });

        public override void OnOpen()
        {
            this.subscription = this.HttpRequestUri.Query.Split(new char[] { '?', '=', '+' }, StringSplitOptions.RemoveEmptyEntries);
            if (this.subscription.Length > 1)
            {
                this.timer = new Timer(this.TimerCallback, null, 0, 1000);
            }

            base.OnOpen();
        }

        protected override void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (this.timer != null)
                    {
                        this.timer.Dispose();
                    }

                    if (this.symbols != null)
                    {
                        this.symbols.Dispose();
                    }
                }

                this.disposed = true;
            }

            base.Dispose(disposing);
        }

        private void TimerCallback(object state)
        {
            string result = string.Empty;
            this.symbols.UpdateValues();
            for (var cnt = 1; cnt < this.subscription.Length; cnt++)
            {
                CompanyInfo info;
                if (this.symbols.TryGetValue(this.subscription[cnt], out info))
                {
                    result += string.Format(CultureInfo.InvariantCulture, "{0} ${1:0.00} ", this.subscription[cnt].ToUpperInvariant(), info.StockPrice);
                }
                else
                {
                    result += string.Format(CultureInfo.InvariantCulture, "{0} not found ", this.subscription[cnt].ToUpperInvariant());
                }
            }

            this.SendMessage(result);
        }
    }
}
