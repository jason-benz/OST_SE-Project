using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediaHub.Data.Model
{
    [Table("UserSuggestion")]
    public class UserSuggestion
    {
        public string UserId1 { get; set; }

        public string UserId2 { get; set; }

#pragma warning disable CS8618
        /// <summary>
        /// Empty ctor needed for EF
        /// </summary>
        protected UserSuggestion() { }
#pragma warning restore CS8618
    }
}