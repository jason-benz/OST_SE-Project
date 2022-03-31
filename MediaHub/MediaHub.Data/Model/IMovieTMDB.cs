namespace MediaHub.Data.Model;

public interface IMovieTMDB
{
    int Id { get; }
    string Title { get; } // in /search is original_title
    string PosterUrl { get; }
    List<string> Genres { get; }
    int Rating { get; }
    string Overview { get; }
    string Runtime { get; } // not in /search 
    string ReleaseDate { get; }
}