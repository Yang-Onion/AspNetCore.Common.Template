using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using ProtoBuf;

namespace AspNetCore.Common.Infrastructure.Extension
{
    public static class CacheExtensions
    {
        public static T Get<T>(this IDistributedCache cache, string key) where T : class {
            byte[] byteArray = cache.Get(key);
            if (byteArray == null || byteArray.Length == 0) {
                return null;
            }

            using (var memoryStream = new MemoryStream(byteArray)) {
                var obj = Serializer.Deserialize<T>(memoryStream);
                return obj;
            }
        }

        public static Task<T> GetAsync<T>(this IDistributedCache cache, string key) where T : class {
            return Task.FromResult(cache.Get<T>(key));
        }

        public static void SetAs<T>(this IDistributedCache cache, string key, T value, TimeSpan? tiemspan = null)
            where T : class {
            try {
                using (var memoryStream = new MemoryStream()) {
                    Serializer.Serialize(memoryStream, value);
                    byte[] byteArray = memoryStream.ToArray();
                    if (tiemspan.HasValue) {
                        var options = new DistributedCacheEntryOptions();
                        options.SetAbsoluteExpiration(tiemspan.Value);
                        cache.Set(key, byteArray, options);
                    }
                    else {
                        cache.SetAs(key, byteArray);
                    }
                }
            }
            catch (Exception) {
                throw;
            }
        }

        public static Task SetAsync<T>(this IDistributedCache cache, string key, T value, TimeSpan? tiemspan = null)
            where T : class {
            return Task.Run(() => cache.SetAs(key, value, tiemspan));
        }

        public static void SetInt32(this IDistributedCache cache, string key, int value, TimeSpan? tiemspan = null) {
            byte[] byteArray =
            {
                (byte) (value >> 24), (byte) (255 & value >> 16), (byte) (255 & value >> 8),
                (byte) (255 & value)
            };
            if (tiemspan.HasValue) {
                var options = new DistributedCacheEntryOptions();
                options.SetAbsoluteExpiration(tiemspan.Value);
                cache.Set(key, byteArray, options);
            }
            else {
                cache.Set(key, byteArray);
            }
        }

        public static int? GetInt32(this IDistributedCache cache, string key) {
            byte[] array = cache.Get(key);
            if (array == null || array.Length < 4) {
                return default(int?);
            }
            return array[0] << 24 | array[1] << 16 | array[2] << 8 | array[3];
        }

        public static void SetString(this IDistributedCache cache, string key, string value, TimeSpan? tiemspan = null) {
            if (tiemspan.HasValue) {
                var options = new DistributedCacheEntryOptions();
                options.SetAbsoluteExpiration(tiemspan.Value);
                cache.Set(key, Encoding.UTF8.GetBytes(value), options);
            }
            else {
                cache.SetAs(key, Encoding.UTF8.GetBytes(value));
            }
        }
    }
}