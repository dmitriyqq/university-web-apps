using System.Collections.Generic;
using MoviePicker.Models;

namespace MoviePicker.Services
{
    public interface IMovieCollectionsHistoryService : IBaseService<MovieCollectionHistoryItem>
    {
        List<MovieCollectionHistoryItem> GetCollectionHistory(string collectionId, Paging paging);
    }
}