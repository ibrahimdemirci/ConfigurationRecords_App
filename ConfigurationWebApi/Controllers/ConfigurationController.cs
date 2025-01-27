using ConfigurationWebApi.Repositories;
using ConfigurationLibrary.Models;
using Microsoft.AspNetCore.Mvc;


namespace ConfigurationWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfigurationRepository _repository;

        public ConfigurationController(IConfigurationRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{applicationName}")]
        public async Task<IActionResult> Get(string applicationName)
        {
            var configurations = await _repository.GetConfigurations(applicationName);
            return Ok(configurations);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ConfigurationItem configurationItem)
        {
            await _repository.SetConfiguration(configurationItem);
            return Ok();
        }
    }

}
