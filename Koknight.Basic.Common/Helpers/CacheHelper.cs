/*************************************************************************************
 *
 * File name:   CacheHelper.cs
 * Description: 
 * 
 * Version：  V1.0
 * Creator  Eason Huang
 * Create time：  2024/11/14 21:54
 * ======================================
*************************************************************************************/
using Microsoft.Extensions.Caching.Memory;

namespace Koknight.Basic.Common.Helpers
{
    /// <summary>
    /// Cache helper
    /// </summary>
    public class Cachehelper
    {
        //Memory cache
        private static IMemoryCache _meoryCache;
        public Cachehelper(IMemoryCache memoryCache)
        {
            _meoryCache = memoryCache;
        }
        /// <summary>
        /// Set cache.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public static bool StringSet<T>(string key, T value, TimeSpan? expiry = null)
        {
            if (expiry == null)
            {
                _meoryCache.Set<T>(key, value);
            }
            else
            {
                _meoryCache.Set<T>(key, value, (TimeSpan)expiry);
            }
            return true;
        }
        /// <summary>
        /// Get cache.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T StringGet<T>(string key)
        {
            T result = _meoryCache.Get<T>(key);
            return result;
        }
        public static void DeleteKey(string key)
        {
            _meoryCache.Remove(key);
        }
    }
}