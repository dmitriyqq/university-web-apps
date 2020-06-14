using Microsoft.Extensions.Logging;
using MoviePicker.Controllers;
using Moq;
using Xunit;
using MoviePicker.Services;
using MoviePicker.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MoviePicker.Tests
{
    public class UsersControllerShould
    {
        private readonly Mock<ILogger<UsersController>> loggerMock;

        public UsersControllerShould()
        {
            this.loggerMock = new Mock<ILogger<UsersController>>();
        }

        [Theory]
        [InlineData("111111111111111111111111", "Name1")]
        [InlineData("111111111111111111111112", "Name1Name1Name1Name1Name1Name1Name1Name1Name1Name1Name1Name1")]
        [InlineData("111111111111111111111113", "123123123Name1")]
        public void ReturnSingleUser(string Id, string Name)
        {
            var userService = new Mock<IUsersService>();
            var omdbService = new Mock<IOMDBService>();
            var movieCollectionsService = new Mock<IMovieCollectionsService>();
            var moviesService = new Mock<IMoviesService>();

            userService.Setup(m => m
                .Get(It.Is<string>(s => s.Equals(Id))))
                .Returns(new User() { Id = Id, Name = Name});
            
            var sut = new UsersController(loggerMock.Object, userService.Object, movieCollectionsService.Object, moviesService.Object, omdbService.Object);
            dynamic result = sut.GetOne(Id);

            userService.Verify(m => m.Get(It.IsAny<string>()), Times.AtLeastOnce);

            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode.Value);

            Assert.IsType<GenericResponse<User>>(result.Value);
            var response = result.Value as GenericResponse<User>;

            Assert.Equal(true, response.Ok);
            Assert.Equal(Name, response.Payload.Name);
            Assert.Equal(Id, response.Payload.Id);
            Assert.Null(response.Paging);
            Assert.Null(response.Error);
        }


    }
}
