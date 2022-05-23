using MediaHub.Data.MediaModule.Model;
using System.Collections.Generic;
using Xunit;

namespace MediaHub.Test.MediaRatingTest
{
    public class MovieTest
    {
        [Theory, Trait("Category", "Unit")]
        [InlineData(1, 1, true, true, true)]
        [InlineData(1, 1, false, false, true)]
        [InlineData(1, 1, true, false, false)]
        [InlineData(1, 1, false, true, false)]
        [InlineData(1, 2, false, false, false)]
        public void MovieEquals(int id1, int id2, bool loadGenre1, bool loadGenre2, bool expectedResult)
        {
            var movie1 = CreateMovie(id1, loadGenre1);
            var movie2 = CreateMovie(id2, loadGenre2);
            Assert.Equal(movie1.Equals(movie2), expectedResult);
        }

        [Fact]
        public void MovieEquals_Null()
        {
            var movie1 = CreateMovie(1, true);
            Movie? movie2 = null;
            Assert.False(movie1.Equals(movie2));
        }

        [Theory, Trait("Category", "Unit")]
        [InlineData(1, 1, true)]
        [InlineData(1, 2, false)]
        public void MovieGetHashCode(int id1, int id2, bool expectedResult)
        {
            var movie1 = CreateMovie(id1, true);
            var movie2 = CreateMovie(id2, true);

            var hashCode1 = movie1.GetHashCode();
            var hashCode2 = movie2.GetHashCode();

            Assert.Equal(hashCode1 == hashCode2, expectedResult);
        }

        private static Movie CreateMovie(int id, bool loadGenre)
        {
            var movie = new Movie(id)
            {
                Title = "Title",
                PosterUrl = "URL",
                Rating = 1,
                Overview = "Overview",
                Runtime = "Runtime",
                Genres = new List<string>(),
                ReleaseDate = "ReleaseDate"
            };

            if (loadGenre)
            {
                movie.Genres.Add("Genre1");
                movie.Genres.Add("Genre2");
            }

            return movie;
        }
    }
}
