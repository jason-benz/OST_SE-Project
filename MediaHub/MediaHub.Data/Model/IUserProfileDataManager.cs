namespace MediaHub.Data.Model
{
    public interface IUserProfileDataManager
    {
        public UserProfile? GetUserProfileById(string userId);

        public UserProfile? GetUserProfileByUsername(string username);

        public void UpdateUserProfile(UserProfile userProfile);

        public bool IsUsernameAvailable(string username);

        public List<UserProfile> GetAllUserProfiles();
    }
}
