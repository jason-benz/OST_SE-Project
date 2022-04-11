namespace MediaHub.Data.Model;

public class Movie : IMovie
{
    public Movie(int id, string title, string posterUrl, List<string> genres, int rating, string overview, string runtime, string releaseDate)
    {
        Id = id;
        Title = title;
        PosterUrl = posterUrl;
        Genres = genres;
        Rating = rating;
        Overview = overview;
        Runtime = runtime;
        ReleaseDate = releaseDate;
    }

    public int Id { get; }
    public string Title { get; }
    public string PosterUrl { get; }
    public List<string> Genres { get; }
    public int Rating { get; }
    public string Overview { get; }
    public string Runtime { get; }
    public string ReleaseDate { get; }

    public string Url { get { return "movieDetailView/" + this.Id; } }

    public override bool Equals(object? obj)
    {
        if ((obj == null) || !this.GetType().Equals(obj.GetType()))
        {
            return false;
        }

        Movie other = (Movie)obj;
        return (Id == other.Id)
               && (Title == other.Title)
               && (PosterUrl == other.PosterUrl)
               && (Genres.All(other.Genres.Contains))
               && (Rating == other.Rating)
               && (Overview == other.Overview)
               && (Runtime == other.Runtime)
               && (ReleaseDate == other.ReleaseDate);
    }

    public override int GetHashCode() => HashCode.Combine(Id, Title, Overview);
}