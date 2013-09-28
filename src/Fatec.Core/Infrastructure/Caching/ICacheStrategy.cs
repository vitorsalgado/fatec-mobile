using System;

namespace Fatec.Core.Infrastructure.Caching
{
	public interface ICacheStrategy
	{
		T Get<T>(string key);
		T Get<T>(string key, Func<T> fetchFunction);
		T Get<T>(string key, int cacheDuration, Func<T> fetchFunction);
		void Add(string key, object data, int cacheDuration);
		bool Contains(string key);
		void Remove(string key);
		void RemoveByPattern(string pattern);
		void ClearAll();
	}
}
