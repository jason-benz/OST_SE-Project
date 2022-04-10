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
}