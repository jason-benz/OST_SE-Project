namespace MediaHub.Data.Model;

public interface IMediaApi
{
    public IMovieTMDB GetMovieById(int id);
    public List<IMovieTMDB> Search(string query);
}