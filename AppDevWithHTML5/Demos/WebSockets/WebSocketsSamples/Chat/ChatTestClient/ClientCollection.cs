// <copyright file="ClientCollection.cs" company="Microsoft Corporation">
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.ServiceModel.WebSockets.Samples
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// Manages a list of <see cref="Client"/> instances.
    /// </summary>
    internal class ClientCollection
    {
        private Collection<Client> innerCollection = new Collection<Client>();
        private object thisLock = new object();
        private bool isFull;
        private int capacity;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientCollection"/> class.
        /// </summary>
        /// <param name="capacity">Number of clients to manage.</param>
        public ClientCollection(int capacity)
        {
            this.capacity = capacity;
        }

        /// <summary>
        /// Gets a value indicating whether the collection is at capacity.
        /// </summary>
        public bool IsFull
        {
            get { return this.isFull; }
        }

        /// <summary>
        /// Add a new client to the collection.
        /// </summary>
        /// <param name="entry">Client to add.</param>
        public void Add(Client entry)
        {
            lock (this.thisLock)
            {
                this.innerCollection.Add(entry);
                this.isFull = this.innerCollection.Count >= this.capacity;
            }
        }

        /// <summary>
        /// Removes a client from the collection.
        /// </summary>
        /// <param name="entry">Client to remove.</param>
        /// <returns>true if client was removed; false otherwise.</returns>
        public bool Remove(Client entry)
        {
            lock (this.thisLock)
            {
                var result = this.innerCollection.Remove(entry);
                this.isFull = this.innerCollection.Count >= this.capacity;
                return result;
            }
        }
    }
}
