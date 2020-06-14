using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Options;

using MongoDB.Driver;

using MoviePicker.Config;
using MoviePicker.Models;

namespace MoviePicker.Services
{
    public class MoviesService : BaseService<Movie>, IMoviesService
    {
        public MoviesService(IOptions<MongoOptions> settings) : base(settings, "movies")
        {
        }

        public List<Movie> GetMoviesInCollection(string collectionId, Paging paging) {
            var movieCollections = _collection.Find(movie =>
                movie.CollectionId == collectionId);

                paging.TotalCount = movieCollections.CountDocuments();

            return movieCollections.Skip(paging.Skip).Limit(paging.Take).ToList();
        }
    }
}