using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bUtility.LocalTokenCache
{
    internal class RecentlyUsedSessionSecurityTokenCache : DistributedSessionSecurityTokenCache
    {
#pragma warning disable 1591
        public const int DefaultTokenCacheSize = 20000;
        public static readonly TimeSpan DefaultPurgeInterval = TimeSpan.FromMinutes(15);
#pragma warning restore 1591

        private readonly Dictionary<SessionSecurityTokenCacheKey, CacheEntry> _items;
        private readonly int _maximumSize;
        private CacheEntry mruEntry;
        private readonly LinkedList<SessionSecurityTokenCacheKey> _mruList;
        private readonly int _sizeAfterPurge;
        private readonly object _syncRoot = new object();

        /// <summary>
        /// Constructor to create an instance of this class.
        /// </summary>
        /// <remarks>
        /// Uses the default maximum cache size.
        /// </remarks>
        public RecentlyUsedSessionSecurityTokenCache()
            : this(DefaultTokenCacheSize)
        {
        }

        /// <summary>
        /// Constructor to create an instance of this class.
        /// </summary>
        /// <param name="maximumSize">Defines the maximum size of the cache.</param>
        public RecentlyUsedSessionSecurityTokenCache(int maximumSize)
            : this(maximumSize, null)
        {
        }

        /// <summary>
        /// Constructor to create an instance of this class.
        /// </summary>
        /// <param name="maximumSize">Defines the maximum size of the cache.</param>
        /// <param name="comparer">The method used for comparing cache entries.</param>
        public RecentlyUsedSessionSecurityTokenCache(int maximumSize, IEqualityComparer<SessionSecurityTokenCacheKey> comparer)
            : this((maximumSize / 5) * 4, maximumSize, comparer)
        {
        }

        /// <summary>
        /// Constructor to create an instance of this class.
        /// </summary>
        /// <param name="sizeAfterPurge">
        /// If the cache size exceeds <paramref name="maximumSize"/>, 
        /// the cache will be resized to <paramref name="sizeAfterPurge"/> by removing least recently used items.
        /// </param>
        /// <param name="maximumSize">Defines the maximum size of the cache.</param>
        public RecentlyUsedSessionSecurityTokenCache(int sizeAfterPurge, int maximumSize)
            : this(sizeAfterPurge, maximumSize, null)
        {
        }

        /// <summary>
        /// Constructor to create an instance of this class.
        /// </summary>
        /// <param name="sizeAfterPurge">Specifies the size to which the cache is purged after it reaches <paramref name="maximumSize"/>.</param>
        /// <param name="maximumSize">Specifies the maximum size of the cache.</param>
        /// <param name="comparer">Specifies the method used for comparing cache entries.</param>
        public RecentlyUsedSessionSecurityTokenCache(int sizeAfterPurge, int maximumSize, IEqualityComparer<SessionSecurityTokenCacheKey> comparer)
        {
            if (sizeAfterPurge < 0)
            {
                throw new ArgumentException("sizeAfterPurge must be non negative", "sizeAfterPurge");
            }

            if (sizeAfterPurge >= maximumSize)
            {
                throw new ArgumentException("sizeAfterPurge must not be smaller than the maximumSize", "sizeAfterPurge");
            }

            maximumSize = maximumSize > 0 ? maximumSize : DefaultTokenCacheSize;
            sizeAfterPurge = sizeAfterPurge > 0 ? sizeAfterPurge : (maximumSize / 5) * 4;
            // null comparer is ok
            this._items = new Dictionary<SessionSecurityTokenCacheKey, CacheEntry>(maximumSize, comparer);
            this._maximumSize = maximumSize;
            this._mruList = new LinkedList<SessionSecurityTokenCacheKey>();
            this._sizeAfterPurge = sizeAfterPurge;
            this.mruEntry = new CacheEntry();
        }

        /// <summary>
        /// Gets the maximum size of the cache
        /// </summary>
        public int MaximumSize
        {
            get { return this._maximumSize; }
        }

        /// <summary>
        /// Deletes the specified cache entry from the MruCache.
        /// </summary>
        /// <param name="key">Specifies the key for the entry to be deleted.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="key"/> is null.</exception>
        public override void Remove(SessionSecurityTokenCacheKey key)
        {
            if (key == null)
            {
                return;
            }

            lock (this._syncRoot)
            {
                CacheEntry entry;
                if (this._items.TryGetValue(key, out entry))
                {
                    this._items.Remove(key);
                    this._mruList.Remove(entry.Node);
                    if (object.ReferenceEquals(this.mruEntry.Node, entry.Node))
                    {
                        this.mruEntry.Value = null;
                        this.mruEntry.Node = null;
                    }
                }
            }
        }

        /// <summary>
        /// Attempts to add an entry to the cache or update an existing one.
        /// </summary>
        /// <param name="key">The key for the entry to be added.</param>
        /// <param name="value">The security token to be added to the cache.</param>
        /// <param name="expirationTime">The expiration time for this entry.</param>
        public override void AddOrUpdate(SessionSecurityTokenCacheKey key, SessionSecurityToken value, DateTime expirationTime)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            lock (this._syncRoot)
            {
                this.Purge();
                this.Remove(key);

                // Add  the new entry to the cache and make it the MRU element
                CacheEntry entry = new CacheEntry();
                entry.Node = this._mruList.AddFirst(key);
                entry.Value = value;
                this._items.Add(key, entry);
                this.mruEntry = entry;
            }
        }

        /// <summary>
        /// Returns the Session Security Token corresponding to the specified key exists in the cache. Also if it exists, marks it as MRU. 
        /// </summary>
        /// <param name="key">Specifies the key for the entry to be retrieved.</param>
        /// <returns>Returns the Session Security Token from the cache if found, otherwise, null.</returns>
        public override SessionSecurityToken Get(SessionSecurityTokenCacheKey key)
        {
            if (key == null)
            {
                return null;
            }

            // If found, make the entry most recently used
            SessionSecurityToken sessionToken = null;
            CacheEntry entry;
            bool found;

            lock (this._syncRoot)
            {
                // first check our MRU item
                if (this.mruEntry.Node != null && key != null && key.Equals(this.mruEntry.Node.Value))
                {
                    return this.mruEntry.Value;
                }

                found = this._items.TryGetValue(key, out entry);
                if (found)
                {
                    sessionToken = entry.Value;

                    // Move the node to the head of the MRU list if it's not already there
                    if (this._mruList.Count > 1 && !object.ReferenceEquals(this._mruList.First, entry.Node))
                    {
                        this._mruList.Remove(entry.Node);
                        this._mruList.AddFirst(entry.Node);
                        this.mruEntry = entry;
                    }
                }
            }

            return sessionToken;
        }

        /// <summary>
        /// Deletes matching cache entries from the MruCache.
        /// </summary>
        /// <param name="endpointId">Specifies the endpointId for the entries to be deleted.</param>
        /// <param name="contextId">Specifies the contextId for the entries to be deleted.</param>
        public override void RemoveAll(string endpointId, System.Xml.UniqueId contextId)
        {
            if (null == contextId || string.IsNullOrEmpty(endpointId))
            {
                return;
            }

            Dictionary<SessionSecurityTokenCacheKey, CacheEntry> entriesToDelete = new Dictionary<SessionSecurityTokenCacheKey, CacheEntry>();
            SessionSecurityTokenCacheKey key = new SessionSecurityTokenCacheKey(endpointId, contextId, null);
            key.IgnoreKeyGeneration = true;
            lock (this._syncRoot)
            {
                foreach (SessionSecurityTokenCacheKey itemKey in this._items.Keys)
                {
                    if (itemKey.Equals(key))
                    {
                        entriesToDelete.Add(itemKey, this._items[itemKey]);
                    }
                }

                foreach (SessionSecurityTokenCacheKey itemKey in entriesToDelete.Keys)
                {
                    this._items.Remove(itemKey);
                    CacheEntry entry = entriesToDelete[itemKey];
                    this._mruList.Remove(entry.Node);
                    if (object.ReferenceEquals(this.mruEntry.Node, entry.Node))
                    {
                        this.mruEntry.Value = null;
                        this.mruEntry.Node = null;
                    }
                }
            }
        }

        /// <summary>
        /// Attempts to remove all entries with a matching endpoint Id from the cache.
        /// </summary>
        /// <param name="endpointId">The endpoint id for the entry to be removed.</param>
        public override void RemoveAll(string endpointId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns all the entries that match the given key.
        /// </summary>
        /// <param name="endpointId">The endpoint id for the entries to be retrieved.</param>
        /// <param name="contextId">The context id for the entries to be retrieved.</param>
        /// <returns>A collection of all the matching entries, an empty collection of no match found.</returns>
        public override IEnumerable<SessionSecurityToken> GetAll(string endpointId, System.Xml.UniqueId contextId)
        {
            Collection<SessionSecurityToken> tokens = new Collection<SessionSecurityToken>();

            if (null == contextId || string.IsNullOrEmpty(endpointId))
            {
                return tokens;
            }

            CacheEntry entry;
            SessionSecurityTokenCacheKey key = new SessionSecurityTokenCacheKey(endpointId, contextId, null);
            key.IgnoreKeyGeneration = true;

            lock (this._syncRoot)
            {
                foreach (SessionSecurityTokenCacheKey itemKey in this._items.Keys)
                {
                    if (itemKey.Equals(key))
                    {
                        entry = this._items[itemKey];

                        // Move the node to the head of the MRU list if it's not already there
                        if (this._mruList.Count > 1 && !object.ReferenceEquals(this._mruList.First, entry.Node))
                        {
                            this._mruList.Remove(entry.Node);
                            this._mruList.AddFirst(entry.Node);
                            this.mruEntry = entry;
                        }

                        tokens.Add(entry.Value);
                    }
                }
            }

            return tokens;
        }

        /// <summary>
        /// This method must not be called from within a read or writer lock as a deadlock will occur.
        /// Checks the time a decides if a cleanup needs to occur.
        /// </summary>
        private void Purge()
        {
            if (this._items.Count >= this._maximumSize)
            {
                // If the cache is full, purge enough LRU items to shrink the 
                // cache down to the low watermark
                int countToPurge = this._maximumSize - this._sizeAfterPurge;
                for (int i = 0; i < countToPurge; i++)
                {
                    SessionSecurityTokenCacheKey keyRemove = this._mruList.Last.Value;
                    this._mruList.RemoveLast();
                    this._items.Remove(keyRemove);
                }
            }
        }

        public class CacheEntry
        {
            public SessionSecurityToken Value
            {
                get;
                set;
            }

            public LinkedListNode<SessionSecurityTokenCacheKey> Node
            {
                get;
                set;
            }
        }
    }
}
