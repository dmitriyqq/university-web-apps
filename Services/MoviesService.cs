using Microsoft.Extensions.Options;
using MoviePicker.Config;
using MoviePicker.Models;

namespace MoviePicker.Services
{
    public class MoviesService : BaseService<Movie>
    {
        public MoviesService(IOptions<MongoOptions> settings) : base(settings, "movies")
        {
        }
    }
}