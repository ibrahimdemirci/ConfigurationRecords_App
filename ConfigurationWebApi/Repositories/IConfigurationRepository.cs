
using ConfigurationLibrary.Models;

namespace ConfigurationWebApi.Repositories
{
    public interface IConfigurationRepository
    {
        Task<IEnumerable<ConfigurationItem>> GetConfigurations(string applicationName);
        Task SetConfiguration(ConfigurationItem configurationItem);
    }
}
