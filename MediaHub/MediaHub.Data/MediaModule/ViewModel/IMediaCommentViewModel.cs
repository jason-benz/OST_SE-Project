using MediaHub.Data.MediaModule.Model;

namespace MediaHub.Data.MediaModule.ViewModel;

public interface IMediaCommentViewModel
{
    public List<MediaComment> GetComments(int mediaId, string userId);

    public void AddComment(string text);

    public void UpdateComment(int Id, string text);
}