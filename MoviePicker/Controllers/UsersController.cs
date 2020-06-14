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
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUsersService _usersService;
        private readonly IMovieCollectionsService _collectionsService;
        private readonly IMoviesService _moviesService;
        private readonly IOMDBService _omdbService;

        public UsersController(ILogger<UsersController> logger,
            IUsersService usersService,
            IMovieCollectionsService collectionsService,
            IMoviesService moviesService,
            IOMDBService OMDBService
        )
        {
            _logger = logger;
            _usersService = usersService;
            _collectionsService = collectionsService;
            _moviesService = moviesService;
            _omdbService = OMDBService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] int? skip, [FromQuery] int? take)
        {
            var paging = new Paging(skip, take);

            if (!this.ValidatePaging(paging))
            {
                return this.OkBadRequest(nameof(take));
            }

            var users = _usersService.Get(paging);
            return this.OkCollection(users, paging);
        }

        [HttpGet("{userId}")]
        public IActionResult GetOne([FromRoute] string userId)
        {
            var user = _usersService.Get(userId);

            if (user == null)
            {
                return this.OkNotFound(nameof(User), userId);
            }

            return this.OkJson(user);
        }

        [HttpGet("{userId}/collections")]
        public IActionResult GetCollections([FromRoute] string userId, [FromQuery] int? skip, [FromQuery] int? take)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return this.OkBadRequest(nameof(userId));
            }

            var paging = new Paging(skip, take);
            if (!this.ValidatePaging(paging))
            {
                return this.OkBadRequest(nameof(take));
            }

            var movieCollections = _collectionsService.GetUserCollections(userId, paging);

            return this.OkCollection(movieCollections, paging);
        }

        [HttpGet("{userId}/collections/{collectionId}/history")]
        public IActionResult GetCollectionHistory([FromRoute] string userId, [FromRoute] string collectionId, [FromQuery] int? skip = 0, [FromQuery] int? take = 10)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return this.OkBadRequest(nameof(userId));
            }
            if (string.IsNullOrWhiteSpace(collectionId))
            {
                return this.OkBadRequest(nameof(collectionId));
            }

            var paging = new Paging(skip, take);
            if (!this.ValidatePaging(paging))
            {
                return this.OkBadRequest(nameof(take));
            }

            var movieCollection = _collectionsService.GetCollectionWithHistory(collectionId, paging);

            if (movieCollection == null)
            {
                return this.OkNotFound(nameof(MovieCollection), collectionId);
            }

            return this.OkCollection(movieCollection, paging);
        }

        [HttpGet("{userId}/collections/{collectionId}/movies")]
        public IActionResult GetCollectionMovies([FromRoute] string userId, [FromRoute] string collectionId, [FromQuery] int? skip = 0, [FromQuery] int? take = 10)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return this.OkBadRequest(nameof(userId));
            }
            if (string.IsNullOrWhiteSpace(collectionId))
            {
                return this.OkBadRequest(nameof(collectionId));
            }

            var paging = new Paging(skip, take);
            if (!this.ValidatePaging(paging))
            {
                return this.OkBadRequest(nameof(take));
            }

            var movieCollection = _collectionsService.GetCollectionWithMovies(collectionId, paging);
            if (movieCollection == null)
            {
                return this.OkNotFound(nameof(MovieCollection), collectionId);
            }

            return this.OkCollection(movieCollection, paging);
        }

        [HttpPost("{userId}/collections")]
        public IActionResult PostCollection([FromRoute] string userId, [FromBody] CreateMovieCollectionRequest createCollectionRequest)
        {
            if (string.IsNullOrWhiteSpace(createCollectionRequest.CollectionName))
            {
                return this.OkBadRequest(nameof(CreateMovieCollectionRequest.CollectionName));
            }

            var user = _usersService.Get(userId);
            if (user == null)
            {
                return this.OkNotFound(nameof(User), userId);
            }

            var model = new MovieCollection()
            {
                Name = createCollectionRequest.CollectionName,
                Owner = user,
            };

            var result = _collectionsService.Create(model);

            return this.OkJson(result);
        }

        [HttpPost("{userId}/collections/{collectionId}/movies/")]
        public async Task<IActionResult> PostMovie([FromRoute] string userId, [FromRoute] string collectionId, [FromBody] CreateMovieRequest createMovieRequest)
        {
            if (string.IsNullOrWhiteSpace(createMovieRequest.ImdbId))
            {
                return this.OkBadRequest(nameof(CreateMovieRequest.ImdbId));
            }

            var user = _usersService.Get(userId);
            if (user == null)
            {
                return this.OkNotFound(nameof(User), userId);
            }

            var movieCollection = _collectionsService.Get(collectionId);
            if (movieCollection == null)
            {
                return this.OkNotFound(nameof(MovieCollection), collectionId);
            }

            var model = new Movie()
            {
                AddedBy = user,
                CollectionId = collectionId,
            };

            var movie = await _omdbService.GetMovie(createMovieRequest.ImdbId);
            movie.AddedBy = user;

            var result = _collectionsService.AddMovieToTheCollection(movie, collectionId);

            return this.OkJson(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateUserRequest userRequest)
        {
            var newUser = new User() { Name = userRequest.Name };

            if (string.IsNullOrWhiteSpace(newUser.Name))
            {
                return this.OkBadRequest("Invalid user name");
            }

            return this.OkJson(_usersService.Create(newUser));
        }
    }
}
