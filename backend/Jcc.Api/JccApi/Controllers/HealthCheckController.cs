using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace JccApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class HealthCheckController : ControllerBase
    {
        private readonly ILogger<HealthCheckController> _logger;

        public HealthCheckController(ILogger<HealthCheckController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Helthcheck endpoint called");
            return Ok();
        }

        [HttpGet("version")]
        public IActionResult GetVersion()
        {
            return Ok(Environment.GetEnvironmentVariable("API_VERSION"));
        }
    }
}
