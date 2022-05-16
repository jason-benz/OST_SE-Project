using MediaHub.Data.MediaModule.Model;
using MediaHub.Data.PersistencyLayer;

namespace MediaHub.Data.MediaModule.Persistency;

public class MediaCommentDataManager : IMediaCommentDataManager
{
    private int _mediaId;
    private string _userId;
    private List<MediaComment> _comments;

    public List<MediaComment> MediaComments
    {
        get => _comments;
    }

    public void Load(int mediaId, string userId)
    {
        _mediaId = mediaId;
        _userId = userId;
        LoadComments();
    }

    private void LoadComments()
    {
        using MediaHubDBContext context = new();
        _comments = context.MediaComments
                           .Where(r => r.MediaId == _mediaId)
                           .ToList();
    }

    public void AddComment(string text)
    {
        using MediaHubDBContext context = new();
        var _comment = new MediaComment()
        {
            MediaId = _mediaId,
            UserId = _userId,
            Created = DateTime.UtcNow,
            CommentText = text
        };
        context.MediaComments.Add(_comment);
        context.SaveChanges();
        LoadComments();
    }

    public void UpdateComment(int Id, string text)
    {
        using MediaHubDBContext context = new();
        MediaComment _comment = context.MediaComments
                              .Where(r => r.Id == Id)
                              .First();
        if (_comment.UserId != _userId)
        {
            throw new InvalidOperationException("You are not allowed to edit this comment");
        }
        _comment.CommentText = text;
        _comment.Created = DateTime.UtcNow;
        context.MediaComments.Update(_comment);
        context.SaveChanges();
        LoadComments();
    }
}