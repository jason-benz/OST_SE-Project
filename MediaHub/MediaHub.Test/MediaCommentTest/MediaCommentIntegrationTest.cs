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
        var actualComment = _mediaCommentDataManager.MediaComments.First();
        RemoveCommentsFromDB();

        Assert.Equal(expectedComment.Id, actualComment.Id);
    }

    [Fact]
    public void AddNewCommentToMedia()
    {
        AddCommentToDB();
        _mediaCommentDataManager.Load(mediaId, userId);
        _mediaCommentDataManager.AddComment("Est Lorem Ipsum");
        var mediaComments = _mediaCommentDataManager.MediaComments;
        RemoveCommentsFromDB();

        Assert.Equal(2, mediaComments.Count);
    }

    [Fact]
    public void UpdateExistingCommentOfMedia()
    {
        var comment = AddCommentToDB();
        _mediaCommentDataManager.Load(mediaId, userId);
        _mediaCommentDataManager.UpdateComment(comment.Id, "New Text");
        var commentText = _mediaCommentDataManager.MediaComments.First().CommentText;
        RemoveCommentsFromDB();

        Assert.True(commentText.Equals("New Text"));
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
            context.MediaComments.Remove(comment);
        }
        context.SaveChanges();
    }
    
}