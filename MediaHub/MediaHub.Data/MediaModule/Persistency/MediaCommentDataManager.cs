using MediaHub.Data.MediaModule.Model;
using MediaHub.Data.PersistencyLayer;

namespace MediaHub.Data.MediaModule.Persistency;

public class MediaCommentDataManager : IMediaCommentDataManager
{
    public List<MediaComment> LoadComments(int mediaId)
    {
        using MediaHubDBContext context = new();
        var _comments = context.MediaComments
                           .Where(r => r.MediaId == mediaId)
                           .ToList();
        return _comments;
    }

    public void AddComment(int mediaId, string userId, string text)
    {
        using MediaHubDBContext context = new();
        var _comment = new MediaComment()
        {
            MediaId = mediaId,
            UserId = userId,
            Created = DateTime.UtcNow,
            CommentText = text
        };
        context.MediaComments.Add(_comment);
        context.SaveChanges();
    }

    public void UpdateComment(int Id, string userId, string text)
    {
        using MediaHubDBContext context = new();
        MediaComment _comment = context.MediaComments
                              .Where(r => r.Id == Id)
                              .First();
        if (_comment.UserId != userId)
        {
            throw new InvalidOperationException("You are not allowed to edit this comment");
        }
        _comment.CommentText = text;
        _comment.Created = DateTime.UtcNow;
        context.MediaComments.Update(_comment);
        context.SaveChanges();
    }

    public void DeleteComment(int Id, string userId)
    {
        using MediaHubDBContext context = new();
        MediaComment _comment = context.MediaComments
                              .Where(r => r.Id == Id)
                              .First();
        if (_comment.UserId != userId)
        {
            throw new InvalidOperationException("You are not allowed to delete this comment");
        }
        context.MediaComments.Remove(_comment);
        context.SaveChanges();
    }
}