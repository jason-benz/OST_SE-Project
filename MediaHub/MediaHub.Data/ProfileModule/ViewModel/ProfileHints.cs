using System.Diagnostics.CodeAnalysis;

namespace MediaHub.Data.ProfileModule.ViewModel
{
    [ExcludeFromCodeCoverage]
    public struct ProfileHints
    {
        public static KeyValuePair<string, bool> ChangesSaved => new("Changes were saved successfully", true);
        public static KeyValuePair<string, bool> UsernameTaken => new("Username is already taken! Please choose another one.", false);
        public static KeyValuePair<string, bool> FileUploadStarted => new("File upload started. Please wait.", false);
        public static KeyValuePair<string, bool> FileTooBig => new("File is too big (max. size: 2MB). Please choose another one.", false);
        public static KeyValuePair<string, bool> FileUploadDone => new("File upload is done. Please save the changes.", false);
    }
}
