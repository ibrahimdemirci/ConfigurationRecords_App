
using ConfigurationLibrary.Models;

namespace ConfigurationLibrary.Repositories
{
    public interface IConfigurationRepository
    {
        Task<IEnumerable<ConfigurationItem>> GetConfigurations(string applicationName);
        Task SetConfiguration(ConfigurationItem configurationItem);
    }
}
