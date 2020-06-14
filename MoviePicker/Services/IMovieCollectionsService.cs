using System.Collections.Generic;
using MoviePicker.Models;

namespace MoviePicker.Services
{
    public interface IMovieCollectionsService : IBaseService<MovieCollection>
    {
        List<MovieCollection> GetUserCollections(string userId, Paging paging);
        Movie AddMovieToTheCollection(Movie movie, string collectionId);
        MovieCollection GetCollectionWithHistory(string collectionId, Paging paging);
        MovieCollection GetCollectionWithMovies(string collectionId, Paging paging);
    }
}