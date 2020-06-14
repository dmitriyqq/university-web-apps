using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;
using MoviePicker.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using MoviePicker.Config;
namespace MoviePicker.Services
{
    public interface IOMDBService
    {
        Task<List<Movie>> SearchMovie(string searchQuery, int year = 0);

        Task<Movie> GetMovie(string imdbId);
    }
}