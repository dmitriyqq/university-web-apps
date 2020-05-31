using Microsoft.Extensions.Options;
using MongoDB.Driver;

using System.Collections.Generic;
using System.Linq;

using MoviePicker.Config;
using MoviePicker.Models;

namespace MoviePicker.Services
{
    public class BaseService<T> where T : Model
    {
        protected IMongoCollection<T> _collection { get; }

        public BaseService(IOptions<MongoOptions> settings, string collectionName)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);

            _collection = database.GetCollection<T>(collectionName);
        }

        public List<T> Get(int skip = 0, int take = 10) =>
            _collection.Find(model => true).Skip(skip).Limit(take).ToList();

        public T Get(string id) =>
            _collection.Find(model => model.Id == id).FirstOrDefault();

        public long Count() => _collection.CountDocuments(model => true);

        public T Create(T model)
        {
            _collection.InsertOne(model);
            return model;
        }

        public void Update(string id, T newModel) =>
            _collection.ReplaceOne(model => model.Id == id, newModel);

        public void Remove(T model) =>
            _collection.DeleteOne(x => x.Id == model.Id);

        public void Remove(string id) => 
            _collection.DeleteOne(x => x.Id == id);
    }
}