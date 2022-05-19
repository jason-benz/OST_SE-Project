namespace MediaHub.Data.ProfileModule.Model
{
    public interface IUserProfileDataManager
    {
        public UserProfile? GetUserProfileById(string userId);

        public UserProfile? GetUserProfileByIdLazyLoading(string userId);

        public UserProfile? GetUserProfileByIdNoTracking(string userId);

        public UserProfile? GetUserProfileByUsername(string username);

        public void UpdateUserProfile(UserProfile userProfile);

        public bool IsUsernameAvailable(string username);

        public IEnumerable<UserProfile> GetUserProfilesById(IEnumerable<string> userIds);
    }
}
