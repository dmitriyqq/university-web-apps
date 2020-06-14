using Microsoft.Extensions.Options;
using MongoDB.Driver;

using System.Collections.Generic;
using System.Linq;

using MoviePicker.Config;
using MoviePicker.Models;

namespace MoviePicker.Services
{
    public class BaseService<T> : IBaseService<T> where T : Model
    {
        protected IMongoCollection<T> _collection { get; }

        public BaseService(IOptions<MongoOptions> settings, string collectionName)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);

            _collection = database.GetCollection<T>(collectionName);
        }

        public virtual List<T> Get(Paging paging) {
            var result = _collection.Find(model => true);
            paging.TotalCount = result.CountDocuments();

            return result.Skip(paging.Skip).Limit(paging.Take).ToList();
        }
            

        public virtual T Get(string id) =>
            _collection.Find(model => model.Id == id).FirstOrDefault();

        public virtual long Count() => _collection.CountDocuments(model => true);

        public virtual T Create(T model)
        {
            _collection.InsertOne(model);
            return model;
        }

        public virtual void Update(string id, T newModel) =>
            _collection.ReplaceOne(model => model.Id == id, newModel);

        public virtual void Remove(T model) =>
            _collection.DeleteOne(x => x.Id == model.Id);

        public virtual void Remove(string id) => 
            _collection.DeleteOne(x => x.Id == id);
    }
}