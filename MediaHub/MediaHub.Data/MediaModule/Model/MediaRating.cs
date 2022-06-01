using MediaHub.Data.ProfileModule.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediaHub.Data.MediaModule.Model;

[Table("MediaRating")]
public class MediaRating
{
    [Key]
    public int Id { get; set; }
    public int MovieId { get; set; }
    public string ProfileId { get; set; }
    [ForeignKey(nameof(ProfileId))]
    public UserProfile Profile { get; set; }

    public bool IsAddedToProfile { get; set; }
    private byte _rating;
    private const byte RatingRangeMax = 10;
    public byte Rating
    {
        get => _rating;
        set
        {
            if (value > RatingRangeMax)
            {
                throw new ArgumentOutOfRangeException("Rating", "Rating cannot be greater than 10");
            }
            _rating = value;
        }
    }

#pragma warning disable CS8618
    /// <summary>
    /// Empty ctor needed for EF
    /// </summary>
    public MediaRating() { }
#pragma warning restore CS8618
}