using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediaHub.Data.Model;

[Table("MediaRating")]
public class MediaRating
{
    [Key] 
    public int Id { get; set; }
    [NotMapped]
    public Movie Movie { get; set; }
    public string ProfileId { get; set; }
    [ForeignKey(nameof(ProfileId))]
    public UserProfile Profile { get; set; }

    public bool ShowInProfile { get; set; } = false;
    private byte? _rating;
    private const byte RATING_RANGE_MAX = 10;
    public byte? Rating
    {
        get => _rating;
        set
        {
            if (value > RATING_RANGE_MAX)
            {
                throw new ArgumentOutOfRangeException("Rating cannot be greater than 10");
            }

            _rating = value;
        }
    }
}