// <copyright file="CompanyInfoCollection.cs" company="Microsoft Corporation">
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.ServiceModel.WebSockets.Samples
{
    using System;
    using System.Collections.ObjectModel;

    internal class CompanyInfoCollection : KeyedCollection<string, CompanyInfo>
    {
        public CompanyInfoCollection()
            : base(StringComparer.OrdinalIgnoreCase)
        {
        }

        /// <summary>
        /// Get the key for this entry.
        /// </summary>
        /// <param name="item">Entry to get key for.</param>
        /// <returns>The key value.</returns>
        protected override string GetKeyForItem(CompanyInfo item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            return item.StockSymbol;
        }
    }
}
