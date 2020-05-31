using Microsoft.Extensions.Options;
using MongoDB.Driver;

using System.Collections.Generic;
using System.Linq;

using MoviePicker.Config;
using MoviePicker.Models;

namespace MoviePicker.Services
{
    public class UsersService : BaseService<User>
    {
        public UsersService(IOptions<MongoOptions> settings) : base(settings, "users")
        {
        }
    }
}