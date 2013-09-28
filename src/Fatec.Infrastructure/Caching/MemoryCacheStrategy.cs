using Fatec.Core.Infrastructure.Caching;
using Fatec.Core.Infrastructure.Configuration;
using System;
using System.Runtime.Caching;
using System.Text.RegularExpressions;

namespace Fatec.Infrastructure.Caching
{
	public class MemoryCacheStrategy : ICacheStrategy
	{
		private readonly IConfigurationProvider _configurationProvider;
		private int _defaultCacheTime;
		private static ObjectCache Cache { get { return MemoryCache.Default; } }

		public MemoryCacheStrategy(IConfigurationProvider configurationProvider)
		{
			_configurationProvider = configurationProvider;
			_defaultCacheTime = _configurationProvider.DefaultCacheExpirationTime;
		}

		public T Get<T>(string key)
		{
			return (T)Cache[key];
		}

		public T Get<T>(string key, Func<T> fetchFunction)
		{
			return Get(key, _defaultCacheTime, fetchFunction);
		}

		public T Get<T>(string key, int cacheDuration, Func<T> fetchFunction)
		{
			if (Contains(key))
				return Get<T>(key);
			else
			{
				var result = fetchFunction();
				if (result != null)
					Add(key, result, cacheDuration);

				return result;
			}
		}

		public void Add(string key, object data, int cacheDuration)
		{
			if (data == null) return;

			var policy = new CacheItemPolicy();
			policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheDuration);
			Cache.Add(new CacheItem(key, data), policy);
		}

		public bool Contains(string key)
		{
			return Cache.Contains(key);
		}

		public void Remove(string key)
		{
			Cache.Remove(key);
		}

		public void RemoveByPattern(string pattern)
		{
			var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);

			foreach (var item in Cache)
				if (regex.IsMatch(item.Key))
					Remove(item.Key);
		}

		public void ClearAll()
		{
			foreach (var item in Cache)
				Cache.Remove(item.Key);
		}
	}
}