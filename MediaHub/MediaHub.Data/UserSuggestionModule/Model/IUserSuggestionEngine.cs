namespace MediaHub.Data.Model
{
    public interface IUserSuggestionEngine
    {
        public Task StartUserSuggestionEngine(string userId);
    }
}
