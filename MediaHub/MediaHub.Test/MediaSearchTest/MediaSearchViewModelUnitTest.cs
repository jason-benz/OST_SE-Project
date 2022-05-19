using System;
using System.Collections.Generic;
using Xunit;
using MediaHub.Data.MediaModule.ViewModel;
using MediaHub.Data.MediaModule.Model;
using MediaHub.Test.UserProfileTest;

namespace MediaHub.Test.MediaSearchTests
{
    public class MediaSearchViewModelTest
    {
        private readonly IMediaSearchViewModel _mediaSearchViewModel;
        private readonly IMediaTableViewModel _mediaTableViewModel;
        public MediaSearchViewModelTest()
        {
            _mediaSearchViewModel = new MediaSearchViewModel(new MockMediaApi());
            _mediaTableViewModel = new MediaTableViewModel(new MockMediaApi(), new UserProfileDataManagerMock());
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
            Assert.ThrowsAsync<Exception>(async () =>
            {
                await _mediaSearchViewModel.GetMovieAsync(13);
            });
        }

        [Fact, Trait("Category", "Unit")]
        public void GetMoviesByString()
        {
            List<IMediaTableViewModel.MovieAndRating> movies = _mediaTableViewModel.GetMoviesByNameAsync("MockMovie").Result;
            Assert.Equal(12, movies[0].Movie.Id);
        }

        [Fact, Trait("Category", "Unit")]
        public void GetMoviesByEmptyString()
        {
            List<IMediaTableViewModel.MovieAndRating> movies = _mediaTableViewModel.GetMoviesByNameAsync("").Result;
            Assert.Contains(DateTime.Now.Year.ToString(), movies[0].Movie.Title);
        }

    }
}

