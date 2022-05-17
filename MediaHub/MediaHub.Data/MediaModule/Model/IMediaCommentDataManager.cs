using MediaHub.Data.MediaModule.Model;

namespace MediaHub.Data.MediaModule.Model;

public interface IMediaCommentDataManager
{
    public void Load(int mediaId, string userId);

    public void AddComment(string text);

    public void UpdateComment(int Id, string text);

    public void DeleteComment(int Id);

    public List<MediaComment> MediaComments { get; }
}