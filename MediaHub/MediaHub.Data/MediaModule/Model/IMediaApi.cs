namespace MediaHub.Data.Model;

public interface IMediaApi
{
    public Task<Movie> GetMovieById(int id);
    public Task<List<Movie>> Search(string query);
}