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
    public string CommentText { get; set; }
}
