using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using MoviePicker.Services;
using MoviePicker.Models;

namespace MoviePicker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly UsersService _usersService;

        public UsersController(ILogger<WeatherForecastController> logger, UsersService usersService)
        {
            _logger = logger;
            _usersService = usersService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return this.OkJson(_usersService.Get());
        }

        [HttpPost]
        public IActionResult Post([FromBody] User newUser)
        {
            if (string.IsNullOrWhiteSpace(newUser.Name)) {
                return this.OkBadRequest("Invalid user name");
            }

            return this.OkJson(_usersService.Create(newUser));
        }
    }
}
