using MediaHub.Data.MediaModule.Model;

namespace MediaHub.Data.MediaModule.ViewModel;

public interface IMediaCommentViewModel
{
    public List<MediaComment> GetComments(int mediaId);

    public void AddComment(int mediaId, string userId, string text);

    public void UpdateComment(int Id, string userId, string text);

    public void DeleteComment(int Id, string userId);
}