using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;
using MoviePicker.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using MoviePicker.Config;
using System;

namespace MoviePicker.Services {
    public class OMDBService : IOMDBService {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _apikey;

        public OMDBService(IHttpClientFactory clientFactory, IOptions<OMDBConfig> options)
        {
            _clientFactory = clientFactory;
            _apikey = options?.Value?.ApiKey;
        }

        public async Task<List<Movie>> SearchMovie(string searchQuery, int year = 0) 
        {
            if (string.IsNullOrWhiteSpace(searchQuery)) 
            {
                throw new SystemException("empty search query");
            }

            var url = year == 0 ? 
                $"/?s={searchQuery}&apikey={_apikey}" :
                $"/?s={searchQuery}&y={year}&apikey={_apikey}";

            try {
                var client = _clientFactory.CreateClient("omdb");
                var responseStr = await client.GetStringAsync(url);
                var response =  JsonConvert.DeserializeObject<OMDBSearchResponse>(responseStr);

                return response.Search.Select(s => new Movie() {Title = s.Title, Year = s.Year, Poster = s.Poster, ImdbId = s.ImdbId}).ToList();
            } catch {
                return new List<Movie>();
            }
            
        }

        public async Task<Movie> GetMovie(string imdbId) 
        {
            var url = $"/?i={imdbId}&apikey={_apikey}";

            var client = _clientFactory.CreateClient("omdb");
            var responseStr = await client.GetStringAsync(url);
            var response =  JsonConvert.DeserializeObject<OMDBMovie>(responseStr);

            if (response.Response != "True")
            {
                throw new System.Exception("Response not true");
            }

            var movie = new Movie() 
            {
                Title = response.Title,
                Year = response.Year,
                Runtime = response.Runtime,
                Director = response.Runtime,
                Poster = response.Poster,
                Country = response.Country,
                ImdbRating = response.ImdbRating,
                Link = $"https://www.imdb.com/title/{response.ImdbId}"
            };
            return movie;
        }
    }
}