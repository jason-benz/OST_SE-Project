namespace MediaHub.Data.UserSuggestionModule.Model
{
    public interface IUserSuggestionEngine
    {
        public Task StartUserSuggestionEngine(string userId);
    }
}
