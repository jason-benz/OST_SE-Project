using MediaHub.Data.ViewModel;
using System;
using System.Collections.Generic;
using MediaHub.Data.Model;
using MediaHub.Test.MediaSearchTest;
using Xunit;

namespace MediaHub.Test.MediaSearchTests
{
    public class MediaSearchViewModelTests
    {
        private readonly IMediaSearchViewModel _mediaSearchViewModel;
        public MediaSearchViewModelTests()
        {
            _mediaSearchViewModel = new MediaSearchViewModel(new MockMediaApi());
        }
        
        [Fact, Trait("Category", "Unit")]
        public void GetMovieById()
        {
            var movie = _mediaSearchViewModel.GetMovieAsync(12).Result;
            Assert.Equal(12, movie.Id);
        }

        [Fact, Trait("Category", "Unit")]
        public void GetMovieByIdException()
        {
            Assert.ThrowsAsync<System.Exception>(async() =>
            {
                await _mediaSearchViewModel.GetMovieAsync(13);
            });
        }

        [Fact, Trait("Category", "Unit")]
        public void GetMoviesByString()
        {
            List<IMovie> movies = _mediaSearchViewModel.GetMoviesAsync("MockMovie").Result;
            Assert.Equal(12, movies[0].Id);
        }

        [Fact, Trait("Category", "Unit")]
        public void GetMoviesByEmptyString()
        {
            List<IMovie> movies = _mediaSearchViewModel.GetMoviesAsync("").Result;
            Assert.Contains(DateTime.Now.Year.ToString(), movies[0].Title);
        }

    }
}

