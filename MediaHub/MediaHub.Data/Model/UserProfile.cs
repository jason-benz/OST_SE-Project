using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediaHub.Data.Model
{
    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [Column("Id", TypeName = "NVARCHAR(450)")]
        public string UserId { get; private set; }
        public string Username { get; set; }
        public string Biography { get; set; }
        public string ProfilePicture { get; set; }

#pragma warning disable CS8618
        /// <summary>
        /// Empty ctor needed for EF
        /// </summary>
        protected UserProfile() { }
#pragma warning restore CS8618

        public UserProfile(string userId)
        {
            UserId = userId;
            Username = $"Guest-{Guid.NewGuid()}";
            Biography = string.Empty;
            ProfilePicture = string.Empty;
        }
    }
}
