using Microsoft.Extensions.Options;
using MoviePicker.Config;
using MoviePicker.Models;

namespace MoviePicker.Services
{
    public class MovieCollectionsService : BaseService<MovieCollection>
    {
        public MovieCollectionsService(IOptions<MongoOptions> settings) : base(settings, "movie_collections")
        {
        }
    }
}