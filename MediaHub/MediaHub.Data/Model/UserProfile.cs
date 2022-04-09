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
        public ICollection<MediaRating> Ratings { get; set; }

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
            Ratings = new List<MediaRating>();
        }

        /// <summary>
        /// Compares all UserProfile properties
        /// </summary>
        public override bool Equals(object? obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                UserProfile up = (UserProfile)obj;
                return (UserId == up.UserId) 
                    && (Username == up.Username)
                    && (Biography == up.Biography)
                    && (ProfilePicture == up.ProfilePicture);
            }
        }

        public override int GetHashCode() => HashCode.Combine(UserId, Username, Biography);
    }
}
