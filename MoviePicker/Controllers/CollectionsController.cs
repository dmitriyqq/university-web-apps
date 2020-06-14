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
    [Route("api/[controller]")]
    public class CollectionsController : ControllerBase
    {
        private readonly ILogger<CollectionsController> _logger;
        private readonly UsersService _usersService;
        private readonly MovieCollectionsService _collectionsService;
        private readonly MoviesService _moviesService;

        public CollectionsController(
            ILogger<CollectionsController> logger,
            UsersService usersService,
            MovieCollectionsService collectionsService,
            MoviesService moviesService
        )
        {
            _logger = logger;
            _usersService = usersService;
            _collectionsService = collectionsService;
            _moviesService = moviesService;
        }

        [HttpGet("selectNewMovie")]
        public IActionResult Get()
        {
            return Ok("ok");
        }

    }
}
