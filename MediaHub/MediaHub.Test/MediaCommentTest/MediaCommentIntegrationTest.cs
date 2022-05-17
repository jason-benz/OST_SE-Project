using System;
using System.Linq;
using MediaHub.Data.MediaModule.Model;
using MediaHub.Data.MediaModule.Persistency;
using MediaHub.Data.PersistencyLayer;
using MediaHub.Data.ProfileModule.Model;
using Xunit;

namespace MediaHub.Test.MediaCommentTest;

public class MediaCommentIntegrationTest
{
    private readonly IMediaCommentDataManager _mediaCommentDataManager = new MediaCommentDataManager();
    private string userId;
    private int mediaId;

    [Fact]
    public void LoadCommentsForMedia()
    {
        var expectedComment = AddCommentToDB();
        _mediaCommentDataManager.Load(mediaId, userId);
        var actualComment = _mediaCommentDataManager.MediaComments.First(c => c.UserId == userId);
        RemoveCommentsFromDB();

        Assert.Equal(expectedComment.Id, actualComment.Id);
    }

    [Fact]
    public void AddNewCommentToMedia()
    {
        AddCommentToDB();
        _mediaCommentDataManager.Load(mediaId, userId);
        _mediaCommentDataManager.AddComment("Est Lorem Ipsum");
        int mediaCommentsCount = _mediaCommentDataManager.MediaComments.Count(c => c.UserId == userId);
        RemoveCommentsFromDB();

        Assert.Equal(2, mediaCommentsCount);
    }

    [Fact]
    public void UpdateExistingCommentOfMedia()
    {
        var comment = AddCommentToDB();
        _mediaCommentDataManager.Load(mediaId, userId);
        _mediaCommentDataManager.UpdateComment(comment.Id, "New Text");
        var commentText = _mediaCommentDataManager.MediaComments.First(c => c.Id == comment.Id).CommentText;
        RemoveCommentsFromDB();

        Assert.True(commentText.Equals("New Text"));
    }

    [Fact]
    public void DelteExistingCommentOfMedia()
    {
        var comment = AddCommentToDB();
        _mediaCommentDataManager.Load(mediaId, userId);
        _mediaCommentDataManager.DeleteComment(comment.Id);
        bool commentNotDeleted = _mediaCommentDataManager.MediaComments.Any(c => c.UserId == userId);
        RemoveCommentsFromDB();

        Assert.False(commentNotDeleted);
    }

    private MediaComment AddCommentToDB()
    {
        using MediaHubDBContext context = new();
        userId = new UserProfile("test").UserId;
        mediaId = new Movie(41).Id;
        var comment = new MediaComment()
        {
            MediaId = mediaId,
            UserId = userId,
            Created = DateTime.UtcNow,
            CommentText = "Lorem ipsum est"
        };
        context.MediaComments.Add(comment);
        context.SaveChanges();
        return comment;
    }

    private void RemoveCommentsFromDB()
    {
        using MediaHubDBContext context = new();
        foreach (var comment in context.MediaComments)
        {
            if (comment.UserId == userId) { context.MediaComments.Remove(comment); }
        }
        context.SaveChanges();
    }

}