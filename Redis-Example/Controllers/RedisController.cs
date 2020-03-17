using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Redis_Example
{
    [Route("api/[controller]")]
    public class RedisController : Controller
    {
        private readonly IDistributedCache distributedCache; 
        public RedisController(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }

        [HttpGet]
        public string Get()
        {
            return "Please enter a key at the end of the URL";
        }


        [HttpGet("{someKey}")]
        public async Task<string> Get(string someKey)
        {
            return await GetValue(someKey);
        }

        private async Task<string> GetValue(string someKey)
        {
            var cacheKey = someKey.ToLower();

            //var encodedMovies = await distributedCache.GetAsync(cacheKey);

            
                var value = Encoding.UTF8.GetBytes("someValue");
                var options = new DistributedCacheEntryOptions()
                                .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                                .SetAbsoluteExpiration(DateTime.Now.AddHours(6));
                await distributedCache.SetAsync(cacheKey, value, options);
            
            return value.ToString();
        }
    }
}
