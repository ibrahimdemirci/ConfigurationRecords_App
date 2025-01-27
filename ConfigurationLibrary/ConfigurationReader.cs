using ConfigurationLibrary.Repositories;
using System.Collections.Concurrent;

namespace ConfigurationLibrary
{
    public class ConfigurationReader
    {
        private readonly string _applicationName;
        private readonly IConfigurationRepository _repository;
        private readonly TimeSpan _refreshInterval;
        private ConcurrentDictionary<string, object> _configurations;
        private Timer _timer;

        public ConfigurationReader(string applicationName, IConfigurationRepository repository, int refreshTimerIntervalInMs)
        {
            _applicationName = applicationName;
            _repository = repository;
            _refreshInterval = TimeSpan.FromMilliseconds(refreshTimerIntervalInMs);

            LoadConfigurations().Wait();
            StartRefreshTimer();
        }

        private async Task LoadConfigurations()
        {
            var configs = await _repository.GetConfigurations(_applicationName);
            _configurations = new ConcurrentDictionary<string, object>(
                configs.ToDictionary(c => c.Name, c => ConvertValue(c.Type, c.Value))
            );
        }

        private void StartRefreshTimer()
        {
            _timer = new Timer(async _ => await LoadConfigurations(), null, _refreshInterval, _refreshInterval);
        }

        private object ConvertValue(string type, string value)
        {
            return type switch
            {
                "string" => value,
                "int" => int.Parse(value),
                "bool" => bool.Parse(value),
                "double" => double.Parse(value),
                _ => throw new NotSupportedException($"Unsupported type: {type}")
            };
        }

        public T GetValue<T>(string key)
        {
            if (_configurations.TryGetValue(key, out var value))
            {
                return (T)value;
            }

            throw new KeyNotFoundException($"Key '{key}' not found.");
        }
    }
}
