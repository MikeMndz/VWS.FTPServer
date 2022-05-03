using RepoDb;
using RepoDb.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class CacheHandler : ICache
    {
        private IDictionary data;

        public void Add<T>(string key, T value, int expiration = 180, bool throwException = true)
        {
            if (data == null)
                data = new Dictionary<string, CacheItem<T>>();
            var tempData = (Dictionary<string, CacheItem<T>>)data;
            if (tempData.ContainsKey(key) && throwException)
                throw new ArgumentException("Error");
            data.Add(key, new CacheItem<T>(key, value, expiration));
        }

        public void Add<T>(CacheItem<T> item, bool throwException = true)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            if (data != null)
                data.Clear();
        }

        public bool Contains(string key)
        {
            if (data == null)
                return false;
            var tempData = (Dictionary<string, object>)data;
            if (tempData.ContainsKey(key))
                return true;
            return false;
        }

        public CacheItem<T> Get<T>(string key, bool throwException = true)
        {
            if (data == null)
                return null;
            var tempData = (Dictionary<string, CacheItem<T>>)data;
            if (!tempData.ContainsKey(key) && throwException)
                throw new ArgumentException("Error");
            return (CacheItem<T>)data[key];
        }

        public IEnumerator GetEnumerator()
        {
            if (data == null)
                throw new ArgumentException("Error");
            return data.GetEnumerator();
        }

        public void Remove(string key, bool throwException = true)
        {
            if (data == null)
                throw new ArgumentException("Error");
            var tempData = (Dictionary<string, object>)data;
            if (tempData.ContainsKey(key) && throwException)
                data.Remove(key);
        }
    }
}
