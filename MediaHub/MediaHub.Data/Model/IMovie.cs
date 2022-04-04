namespace MediaHub.Data.Model;

public interface IMovie
{
    int Id { get; }
    string Title { get; } // is called is original_title in search endpoint, in movie endpoint title
    string PosterUrl { get; }
    List<string> Genres { get; }
    int Rating { get; }
    string Overview { get; }
    string Runtime { get; } // not included in /search endpoint of api
    string ReleaseDate { get; }

    public string Url { get; }
}