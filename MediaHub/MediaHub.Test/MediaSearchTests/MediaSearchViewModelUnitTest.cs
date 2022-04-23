using MediaHub.Data.ViewModel;
using System;
using System.Collections.Generic;
using MediaHub.Data.Model;
using Xunit;

namespace MediaHub.Test.MediaSearchTest
{
    public class MediaSearchViewModelTest
    {
        private readonly IMediaSearchViewModel _mediaSearchViewModel;
        public MediaSearchViewModelTest()
        {
            _mediaSearchViewModel = new MediaSearchViewModelMock();
        }
        
        [Fact]
        public void GetMovieById()
        {
            var movie = _mediaSearchViewModel.GetMovieAsync(12).Result;
            Assert.Equal(12, movie.Id);
        }

        [Fact]
        public void GetMovieByIdException()
        {
            Assert.ThrowsAsync<System.Exception>(async() =>
            {
                await _mediaSearchViewModel.GetMovieAsync(13);
            });
        }

        [Fact]
        public void GetMoviesByString()
        {
            List<Movie> movies = _mediaSearchViewModel.GetMoviesAsync("MockMovie").Result;
            Assert.Equal(12, movies[0].Id);
        }

        [Fact]
        public void GetMoviesByEmptyString()
        {
            List<Movie> movies = _mediaSearchViewModel.GetMoviesAsync("").Result;
            Assert.Contains(DateTime.Now.Year.ToString(), movies[0].Title);
        }

    }
}

