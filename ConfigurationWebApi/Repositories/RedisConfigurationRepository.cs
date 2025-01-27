using StackExchange.Redis;
using ConfigurationLibrary.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace ConfigurationWebApi.Repositories
{
    public class RedisConfigurationRepository : IConfigurationRepository
    {
        private readonly IConnectionMultiplexer _redis;

        public RedisConfigurationRepository(string connectionString)
        {
            _redis = ConnectionMultiplexer.Connect(connectionString);
        }
        [HttpGet]
        public async Task<IEnumerable<ConfigurationItem>> GetConfigurations(string applicationName)
        {
            var db = _redis.GetDatabase();
            var keys = _redis.GetServer(_redis.GetEndPoints().First()).Keys(pattern: $"{applicationName}:*");

            var configurations = new List<ConfigurationItem>();
            foreach (var key in keys)
            {
                var json = await db.StringGetAsync(key);
                if (!string.IsNullOrEmpty(json))
                {
                    var item = JsonSerializer.Deserialize<ConfigurationItem>(json);
                    if (item != null && item.IsActive)
                    {
                        configurations.Add(item);
                    }
                }
            }

            return configurations;
        }
        [HttpPost]
        public async Task SetConfiguration(ConfigurationItem configurationItem)
        {
            var db = _redis.GetDatabase();
            var key = $"{configurationItem.ApplicationName}:{configurationItem.Name}";
            var value = JsonSerializer.Serialize(configurationItem);
            await db.StringSetAsync(key, value);
        }
    }
}
