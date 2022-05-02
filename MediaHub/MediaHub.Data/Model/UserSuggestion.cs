using System.ComponentModel.DataAnnotations.Schema;

namespace MediaHub.Data.Model
{
    [Table("UserSuggestion")]
    public class UserSuggestion
    {
        public string UserId1 { get; set; }

        [ForeignKey(nameof(UserId1))]
        public UserProfile UserProfile1 { get; set; }

        public string UserId2 { get; set; }

        [ForeignKey(nameof(UserId2))]
        public UserProfile UserProfile2 { get; set; }

        public bool IgnoreSuggestion { get; set; }
    }
}