using Microsoft.Extensions.Options;
using MoviePicker.Config;
using MoviePicker.Models;

namespace MoviePicker.Services
{
    public class CollectionsService : BaseService<CollectionHistoryItem>
    {
        public CollectionsService(IOptions<MongoOptions> settings) : base(settings, "collection_history")
        {
        }
    }
}