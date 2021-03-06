using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EnvVariable.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnvController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public EnvController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("section")]
        public ActionResult GetBySection()
        {
            return Ok(new Result()
            {
                Key = _configuration.GetSection("DB_KEY").Value,
                Token = _configuration.GetSection("DB_TOKEN").Value,
                Id = int.Parse(_configuration.GetSection("DB_ID").Value),
            });
        }
        
        [HttpGet("value")]
        public ActionResult GetByValue()
        {
            return Ok(new Result()
            {
                Key = _configuration.GetValue<string>("DB_KEY"),
                Token = _configuration.GetValue<string>("DB_TOKEN"),
                Id = _configuration.GetValue<int>("DB_ID"),
            });
        }
    }

    public class Result
    {
        public string Key { get; set; }
        public string Token { get; set; }
        public int Id { get; set; }
    }
}