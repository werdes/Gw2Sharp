using System;

namespace Gw2Sharp.WebApi.Caching
{
    /// <summary>
    /// A default object cache item.
    /// </summary>
    public class CacheItem : IEquatable<CacheItem>
    {
        /// <summary>
        /// Creates a new cache item.
        /// </summary>
        /// <param name="category">The cache category.</param>
        /// <param name="id">The id.</param>
        /// <param name="item">The item.</param>
        /// <param name="expiryTime">The expiry time.</param>
        public CacheItem(string category, string id, object item, DateTimeOffset expiryTime)
        {
            this.Category = category ?? throw new ArgumentNullException(nameof(category));
            this.Id = id ?? throw new ArgumentNullException(nameof(id));
            this.Item = item ?? throw new ArgumentNullException(nameof(item));
            this.ExpiryTime = expiryTime;
        }

        /// <summary>
        /// The category associated with the cache item.
        /// </summary>
        public string Category { get; }

        /// <summary>
        /// The id associated with the cache item.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// The cache item.
        /// </summary>
        public object Item { get; }

        /// <summary>
        /// The time when this cache item expires.
        /// </summary>
        public DateTimeOffset ExpiryTime { get; }

        /// <inheritdoc />
        public override int GetHashCode() =>
            HashCode.Combine(this.Category, this.Id, this.Item, this.ExpiryTime);

        /// <inheritdoc />
        public override bool Equals(object? obj) =>
            obj is CacheItem cacheItem && this.Equals(cacheItem);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// <c>true</c> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool Equals(CacheItem? other) =>
            other != null &&
            Equals(this.Category, other.Category) &&
            Equals(this.Id, other.Id) &&
            Equals(this.Item, other.Item) &&
            Equals(this.ExpiryTime, other.ExpiryTime);
    }

    /// <summary>
    /// A generic cache item.
    /// </summary>
    public class CacheItem<T> : CacheItem, IEquatable<CacheItem<T>>
    {
        /// <summary>
        /// Creates a new cache item.
        /// </summary>
        /// <param name="category">The cache category.</param>
        /// <param name="id">The id.</param>
        /// <param name="item">The item.</param>
        /// <param name="expiryTime">The expiry time.</param>
        public CacheItem(string category, string id, T item, DateTimeOffset expiryTime) : base(category, id, item!, expiryTime) { }

        /// <summary>
        /// The cache item.
        /// </summary>
        public new T Item => (T)base.Item;

        /// <inheritdoc />
        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode());

        /// <inheritdoc />
        public override bool Equals(object? obj) =>
            obj is CacheItem<T> item && this.Equals(item);

        /// <inheritdoc />
        public virtual bool Equals(CacheItem<T>? other) =>
            this.Equals(other as CacheItem);
    }
}
