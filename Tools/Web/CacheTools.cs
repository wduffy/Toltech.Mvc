using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;

namespace Wduffy.Mvc.Tools
{
    
    public static class CacheTools
    {

        private static CacheDependency GetCacheKeyDependancy(string key)
        {
            // Create the array of cache keys
            string[] keys = new string[] {key};

            // Return the cache dependancy
            return new CacheDependency(null, keys);            
        }

        private static void EnsureDependencyKey(string key)
        {
            // If the cache key does not currently exist then create it with a 1 day lifespan
            if (HttpContext.Current.Cache[key] == null)
                HttpContext.Current.Cache.Insert(key, DateTime.Now, null, DateTime.Now.AddDays(1), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.NotRemovable, null);
        }

        public static Cache Cache
        {
            get { return HttpContext.Current.Cache; }
        }

        public static void ResetCacheKey(string key)
        {
            CacheTools.EnsureDependencyKey(key);
            CacheTools.Cache[key] = DateTime.Now;
        }
        
        //
        // Summary:
        //     Inserts an object into the System.Web.Caching.Cache object with dependencies,
        //     expiration and priority policies, and a delegate you can use to notify your
        //     application when the inserted item is removed from the Cache.
        //
        // Parameters:
        //   key:
        //     The cache key used to reference the object.
        //
        //   value:
        //     The object to be inserted in the cache.
        //
        //   dependencies:
        //     The file or cache key dependencies for the item. When any dependency changes,
        //     the object becomes invalid and is removed from the cache. If there are no
        //     dependencies, this parameter contains null.
        //
        //   absoluteExpiration:
        //     The time at which the inserted object expires and is removed from the cache.
        //     To avoid possible issues with local time such as changes from standard time
        //     to daylight saving time, use System.DateTime.UtcNow rather than System.DateTime.Now
        //     for this parameter value. If you are using absolute expiration, the slidingExpiration
        //     parameter must be System.Web.Caching.Cache.NoSlidingExpiration.
        //
        //   slidingExpiration:
        //     The interval between the time the inserted object was last accessed and the
        //     time at which that object expires. If this value is the equivalent of 20
        //     minutes, the object will expire and be removed from the cache 20 minutes
        //     after it was last accessed. If you are using sliding expiration, the absoluteExpiration
        //     parameter must be System.Web.Caching.Cache.NoAbsoluteExpiration.
        //
        //   priority:
        //     The cost of the object relative to other items stored in the cache, as expressed
        //     by the System.Web.Caching.CacheItemPriority enumeration. This value is used
        //     by the cache when it evicts objects; objects with a lower cost are removed
        //     from the cache before objects with a higher cost.
        //
        //   onRemoveCallback:
        //     A delegate that, if provided, will be called when an object is removed from
        //     the cache. You can use this to notify applications when their objects are
        //     deleted from the cache.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     The key or value parameter is null.
        //
        //   System.ArgumentOutOfRangeException:
        //     You set the slidingExpiration parameter to less than TimeSpan.Zero or the
        //     equivalent of more than one year.
        //
        //   System.ArgumentException:
        //     The absoluteExpiration and slidingExpiration parameters are both set for
        //     the item you are trying to add to the Cache.
        public static void Insert(string key, object value, string dependencyKey)
        {
            CacheTools.EnsureDependencyKey(dependencyKey);
            HttpContext.Current.Cache.Insert(key, value, CacheTools.GetCacheKeyDependancy(dependencyKey));
        }

        public static void Insert(string key, object value, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority)
        {
            CacheTools.Insert(key, value, absoluteExpiration, slidingExpiration, priority, null);
        }
        
        public static void Insert(string key, object value, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback)
        {
            HttpContext.Current.Cache.Insert(key, value, null, absoluteExpiration, slidingExpiration, priority, onRemoveCallback);
        }

    }

}
