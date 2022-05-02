namespace MediaHub.Data.Model;

public class Movie 
{
    public Movie(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
    public string? Title { get;  set; }
    public string? PosterUrl { get; set; }
    public List<string>? Genres { get; set; }
    public int? Rating { get; set; }
    public string? Overview { get; set; }
    public string? Runtime { get; set; }
    public string? ReleaseDate { get; set; }

    public override bool Equals(object? obj)
    {
        if ((obj == null) || !this.GetType().Equals(obj.GetType()))
        {
            return false;
        }

        Movie other = (Movie)obj;
        return (Id == other.Id)
               && (String.Equals(Title,other.Title))
               && (String.Equals(PosterUrl, other.PosterUrl))
               && (compareGenres(Genres, other.Genres)) //Genres.All(other.Genres.Contains))
               && (String.Equals(Rating, other.Rating))
               && (String.Equals(Overview, other.Overview))
               && (String.Equals(Runtime, other.Runtime))
               && (String.Equals(ReleaseDate, other.ReleaseDate));
    }

    private bool compareGenres(List<string> genres1, List<string> genres2)
    {
        var firstNotSecond = genres1.Except(genres2).ToList();
        var secondNotFirst = genres2.Except(genres1).ToList();
        return !firstNotSecond.Any() && !secondNotFirst.Any();
    }

    public override int GetHashCode() => HashCode.Combine(Id, Title, Overview);
}