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

        [Column("UserId", TypeName = "NVARCHAR(450)")]
        public string UserId { get; private set; }

        [ForeignKey(nameof(UserId))]
        public UserProfile UserProfile { get; set; }

        public Table ChangedTable { get; set; }

        /// <summary>
        /// Additional information. E.g. Movie title
        /// </summary>
        public string? AdditionalInformation { get; set; }

#pragma warning disable CS8618
        /// <summary>
        /// Empty ctor needed for EF
        /// </summary>
        protected FeedItem() { }
#pragma warning restore CS8618

        public FeedItem(string userId)
        {
            UserId = userId;
            CreationDate = DateTime.Now;
        }

        /// <summary>
        /// Compares all FeedItem properties
        /// </summary>
        public override bool Equals(object? obj)
        {
            if (obj == null || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                FeedItem fi = (FeedItem)obj;
                return Id == fi.Id
                    && CreationDate == fi.CreationDate
                    && UserId == fi.UserId
                    && ChangedTable == fi.ChangedTable
                    && AdditionalInformation == fi.AdditionalInformation;
            }
        }

        public override int GetHashCode() => HashCode.Combine(Id, CreationDate, UserId, ChangedTable, AdditionalInformation);
    }
}
