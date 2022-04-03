namespace MediaHub.Data.Model;

public interface IMediaApi
{
    public Task<IMovie> GetMovieById(int id);
    public Task<List<IMovie>> Search(string query);
}