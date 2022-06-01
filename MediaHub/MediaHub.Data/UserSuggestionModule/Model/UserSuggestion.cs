using MediaHub.Data.ProfileModule.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediaHub.Data.UserSuggestionModule.Model
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

#pragma warning disable CS8618
        /// <summary>
        /// Empty ctor needed for EF
        /// </summary>
        public UserSuggestion() { }
#pragma warning restore CS8618

        /// <summary>
        /// Compares all UserSuggestion properties
        /// </summary>
        public override bool Equals(object? obj)
        {
            if (obj == null || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                UserSuggestion us = (UserSuggestion)obj;
                return UserId1 == us.UserId1
                    && UserId2 == us.UserId2
                    && IgnoreSuggestion == us.IgnoreSuggestion;
            }
        }

        public override int GetHashCode() => HashCode.Combine(UserId1, UserId2, IgnoreSuggestion);
    }
}