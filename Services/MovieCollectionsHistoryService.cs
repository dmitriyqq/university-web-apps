using Microsoft.Extensions.Options;
using MoviePicker.Config;
using MoviePicker.Models;

namespace MoviePicker.Services
{
    public class MovieCollectionsHistoryService : BaseService<CollectionHistoryItem>
    {
        public MovieCollectionsHistoryService(IOptions<MongoOptions> settings) : base(settings, "collection_history")
        {
        }
    }
}