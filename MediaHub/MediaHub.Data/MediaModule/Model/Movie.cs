namespace MediaHub.Data.MediaModule.Model;

public class Movie
{
    public Movie(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
    public string? Title { get; set; }
    public string? PosterUrl { get; set; }
    public List<string>? Genres { get; set; }
    public int? Rating { get; set; }
    public string? Overview { get; set; }
    public string? Runtime { get; set; }
    public string? ReleaseDate { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj == null || !GetType().Equals(obj.GetType()))
        {
            return false;
        }

        Movie other = (Movie)obj;
        return Id == other.Id
               && string.Equals(Title, other.Title)
               && string.Equals(PosterUrl, other.PosterUrl)
               && CompareGenres(Genres!, other.Genres!) //Genres.All(other.Genres.Contains))
               && Equals(Rating, other.Rating)
               && string.Equals(Overview, other.Overview)
               && string.Equals(Runtime, other.Runtime)
               && string.Equals(ReleaseDate, other.ReleaseDate);
    }

    private bool CompareGenres(List<string> genres1, List<string> genres2)
    {
        var firstNotSecond = genres1.Except(genres2).ToList();
        var secondNotFirst = genres2.Except(genres1).ToList();
        return !firstNotSecond.Any() && !secondNotFirst.Any();
    }

    public override int GetHashCode() => HashCode.Combine(Id, Title, Overview);
}