namespace MediaHub.Data.Model
{
    public interface IUserProfileDataManager
    {
        public UserProfile GetUserProfileById(string userId);

        public UserProfile GetUserProfileByUsername(string username);

        public Task<int> UpdateUserProfileAsync(UserProfile userProfile);
    }
}
