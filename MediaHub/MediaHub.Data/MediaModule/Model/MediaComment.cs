using MediaHub.Data.ProfileModule.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediaHub.Data.MediaModule.Model;

[Table("MediaComment")]
public class MediaComment
{
    [Key]
    public int Id { get; set; }
    public int MediaId { get; set; }
    [Column("UserId", TypeName = "NVARCHAR(450)")]
    public string UserId { get; set; }
    public DateTime Created { get; set; }
    private string _commentText = string.Empty;
    private const int CommentMaxChar = 200;
    public string CommentText
    {
        get => _commentText;
        set
        {
            if (value.Length > CommentMaxChar)
            {
                throw new ArgumentOutOfRangeException(value, "Comment has to be less than 200 characters");
            }
            _commentText = value;
        }
    }
}
