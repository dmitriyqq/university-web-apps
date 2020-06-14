using System.Collections.Generic;
using MoviePicker.Models;

namespace MoviePicker.Services
{
    public interface IMoviesService : IBaseService<Movie>
    {
        List<Movie> GetMoviesInCollection(string collectionId, Paging paging);
    }
}