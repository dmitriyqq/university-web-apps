using Microsoft.Extensions.Options;
using MongoDB.Driver;

using System.Collections.Generic;
using System.Linq;

using MoviePicker.Config;
using MoviePicker.Models;

namespace MoviePicker.Services
{
    public interface IBaseService<T> where T : Model
    {
        List<T> Get(Paging paging);

        T Get(string id);

        long Count();

        T Create(T model);

        void Update(string id, T newModel);

        void Remove(T model);

        void Remove(string id);
    }
}