using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EnvVariable.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnvController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ToggleSetting _toggleSetting;

        public EnvController(IConfiguration configuration, ToggleSetting toggleSetting)
        {
            _configuration = configuration;
            _toggleSetting = toggleSetting;
        }

        [HttpGet("section")]
        public ActionResult GetBySection()
        {
            return Ok(new Result()
            {
                Key = _configuration.GetSection("DB:KEY").Value,
                Token = _configuration.GetSection("DB:TOKEN").Value,
                Id = int.Parse(_configuration.GetSection("DB:ID").Value),
            });
        }
        
        [HttpGet("value")]
        public ActionResult GetByValue()
        {
            return Ok(new Result()
            {
                Key = _configuration.GetValue<string>("DB:KEY"),
                Token = _configuration.GetValue<string>("DB:TOKEN"),
                Id = _configuration.GetValue<int>("DB:ID"),
            });
        }
        
        [HttpGet("toggle")]
        public ActionResult GetToggle()
        {
            return Ok(new ToggleResult()
            {
                IsPromotionEnabled = _toggleSetting.IsPromotionEnabled,
                IsNewBackgroundEnabled = _toggleSetting.IsNewBackgroundEnabled,
            });
        }
    }

    public class ToggleResult
    {
        public bool IsPromotionEnabled { get; set; }
        public bool IsNewBackgroundEnabled { get; set; }
    }

    public class Result
    {
        public string Key { get; set; }
        public string Token { get; set; }
        public int Id { get; set; }
    }
    
    public class ToggleSetting
    {
        public bool IsPromotionEnabled { get; set; }
        public bool IsNewBackgroundEnabled { get; set; }
    }
}