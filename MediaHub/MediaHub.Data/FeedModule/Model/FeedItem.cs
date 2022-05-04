using MediaHub.Data.ProfileModule.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediaHub.Data.FeedModule.Model
{
    [Table("FeedItem")]
    public class FeedItem
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreationDate { get; set; }

        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public UserProfile UserProfile { get; set; }

        public Tables ChangedTable { get; set; }

        public string OldValue { get; set; }

        public string NewValue { get; set; }
    }
}
