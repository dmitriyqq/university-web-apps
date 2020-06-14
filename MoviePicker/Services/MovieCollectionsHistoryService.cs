using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Options;

using MongoDB.Driver;

using MoviePicker.Config;
using MoviePicker.Models;

namespace MoviePicker.Services
{
    public class MovieCollectionsHistoryService : BaseService<MovieCollectionHistoryItem>, IMovieCollectionsHistoryService
    {
        public MovieCollectionsHistoryService(IOptions<MongoOptions> settings) : base(settings, "collection_history")
        {
        }

        public List<MovieCollectionHistoryItem> GetCollectionHistory(string collectionId, Paging paging)
        {
            var history = _collection.Find(h => h.CollectionId == collectionId);

            paging.TotalCount = history.CountDocuments();

            return history.Skip(paging.Skip).Limit(paging.Take).ToList();
        }
    }
}