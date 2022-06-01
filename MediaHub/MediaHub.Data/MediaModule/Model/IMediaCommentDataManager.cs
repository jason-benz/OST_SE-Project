namespace MediaHub.Data.MediaModule.Model;

public interface IMediaCommentDataManager
{
    public List<MediaComment> LoadComments(int mediaId);

    public void AddComment(int mediaId, string userId, string text);

    public void UpdateComment(int Id, string userId, string text);

    public void DeleteComment(int Id, string userId);
}