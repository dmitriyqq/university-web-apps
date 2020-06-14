using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using MoviePicker.Services;
using MoviePicker.Models;
using System.Threading.Tasks;

namespace MoviePicker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly ILogger<CollectionsController> _logger;
        private readonly UsersService _usersService;
        private readonly MovieCollectionsService _collectionsService;
        private readonly OMDBService _moviesService;

        public MoviesController(
            ILogger<CollectionsController> logger,
            OMDBService moviesService
        )
        {
            _logger = logger;
            _moviesService = moviesService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]string query, [FromQuery]int year)
        {
            return this.OkJson(await _moviesService.SearchMovie(query, year));
        }

    }
}
