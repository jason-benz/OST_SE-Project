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

#pragma warning disable CS8618
    /// <summary>
    /// Empty ctor needed for EF
    /// </summary>
    public MediaComment() { }
#pragma warning restore CS8618
}
