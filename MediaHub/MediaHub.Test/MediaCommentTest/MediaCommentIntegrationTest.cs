using System;
using System.Linq;
using MediaHub.Data.MediaModule.Model;
using MediaHub.Data.MediaModule.Persistency;
using MediaHub.Data.PersistencyLayer;
using MediaHub.Data.ProfileModule.Model;
using Xunit;

namespace MediaHub.Test.MediaCommentTest;

public class MediaCommentIntegrationTest : IDisposable
{
    private readonly IMediaCommentDataManager _mediaCommentDataManager = new MediaCommentDataManager();
    private readonly string _userId = "test";
    private readonly int _mediaId = 41;

    [Fact]
    public void LoadCommentsForMedia()
    {
        var expectedComment = AddCommentToDBUser();
        var actualComment = _mediaCommentDataManager.LoadComments(_mediaId).First(c => c.MediaId == _mediaId);

        Assert.Equal(expectedComment.Id, actualComment.Id);
    }

    [Fact]
    public void AddNewCommentToMedia()
    {
        AddCommentToDBUser();
        _mediaCommentDataManager.AddComment(_mediaId, _userId, "Add comment");
        int mediaCommentsCount = _mediaCommentDataManager.LoadComments(_mediaId).Count(c => c.MediaId == _mediaId);

        Assert.Equal(2, mediaCommentsCount);
    }

    [Fact]
    public void UpdateExistingCommentOfMedia()
    {
        var comment = AddCommentToDBUser();
        _mediaCommentDataManager.UpdateComment(comment.Id, _userId, "New Text");
        var commentText = _mediaCommentDataManager.LoadComments(_mediaId).First(c => c.Id == comment.Id).CommentText;

        Assert.True(commentText.Equals("New Text"));
    }

    [Fact]
    public void DelteExistingCommentOfMedia()
    {
        var comment = AddCommentToDBUser();
        _mediaCommentDataManager.DeleteComment(comment.Id, _userId);
        bool commentNotDeleted = _mediaCommentDataManager.LoadComments(_mediaId).Any(c => c.MediaId == _mediaId);

        Assert.False(commentNotDeleted);
    }

    private MediaComment AddCommentToDBUser()
    {
        var comment = new MediaComment()
        {
            MediaId = _mediaId,
            UserId = _userId,
            Created = DateTime.UtcNow,
            CommentText = "Lorem ipsum est"
        };
        using MediaHubDBContext context = new();
        context.MediaComments.Add(comment);
        context.SaveChanges();
        return comment;
    }

    public void Dispose()
    {
        using MediaHubDBContext context = new();
        foreach (var comment in context.MediaComments)
        {
            if (comment.UserId == _userId) { context.MediaComments.Remove(comment); }
        }
        context.SaveChanges();
    }
}