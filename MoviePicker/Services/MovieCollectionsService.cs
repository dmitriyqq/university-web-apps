using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Options;

using MongoDB.Driver;

using MoviePicker.Config;
using MoviePicker.Models;

namespace MoviePicker.Services
{
    public class MovieCollectionsService : BaseService<MovieCollection>, IMovieCollectionsService
    {
        private IMovieCollectionsHistoryService _historyService { get; set; }
        private IMoviesService _moviesService { get; set; }

        public MovieCollectionsService(
            IOptions<MongoOptions> settings,
            IMovieCollectionsHistoryService historyService,
            IMoviesService moviesService) : base(settings, "movie_collections")
        {
            _historyService = historyService;
            _moviesService = moviesService;
        }

        public List<MovieCollection> GetUserCollections(string userId, Paging paging)
        {
            var movieCollections = _collection.Find(movieCollection =>
                movieCollection.GroupMemebers.Any(u => u.Id == userId) ||
                movieCollection.Owner.Id == userId);

            paging.TotalCount = movieCollections.CountDocuments();

            return movieCollections.Skip(paging.Skip).Limit(paging.Take).ToList();
        }

        public Movie AddMovieToTheCollection(Movie movie, string collectionId)
        {
            var exitingMovie = _moviesService.Get(movie.Id);
            if (exitingMovie != null)
            {
                throw new System.Exception("Movie already exists in collection");
            }

            var model = new MovieCollectionHistoryItem()
            {
                CollectionId = collectionId,
                Movie = movie,
                User = movie.AddedBy,
                Type = MovieCollectionHistoryItemType.MovieAdded
            };

            movie.CollectionId = collectionId;

            _historyService.Create(model);
            _moviesService.Create(movie);

            return movie;
        }

        public MovieCollection GetCollectionWithHistory(string collectionId, Paging paging)
        {
            var movieCollection = Get(collectionId);

            if (movieCollection == null)
            {
                throw new System.Exception("Movie collection not found");
            }

            var history = _historyService.GetCollectionHistory(collectionId, paging);

            movieCollection.History = history;

            return movieCollection;
        }

        public MovieCollection GetCollectionWithMovies(string collectionId, Paging paging)
        {
            var movieCollection = Get(collectionId);

            if (movieCollection == null)
            {
                throw new System.Exception("Movie collection not found");
            }

            var movies = _moviesService.GetMoviesInCollection(collectionId, paging);

            movieCollection.Movies = movies;

            return movieCollection;
        }
    }
}