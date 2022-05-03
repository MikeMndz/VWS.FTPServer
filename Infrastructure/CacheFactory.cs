using RepoDb;
using RepoDb.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public static class CacheFactory
    {
        private static object _syncLock = new object();
        private static ICache _cache = null;

        public static ICache CreateCacher()
        {
            if (_cache == null)
            {
                lock (_syncLock)
                {
                    if (_cache == null)
                    {
                        _cache = new CacheHandler();
                    }
                }
            }
            return _cache;
        }
    }
}
