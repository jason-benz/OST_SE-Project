using System;
using System.Linq;
using MediaHub.Data.MediaModule.ViewModel;
using MediaHub.Data.PersistencyLayer;
using MediaHub.Test.LogTests;
using Xunit;

namespace MediaHub.Test.MediaCommentTest;

public class MediaCommentViewModelUnitTest
{
    private readonly IMediaCommentViewModel _mediaCommentViewModel;
    public MediaCommentViewModelUnitTest()
    {
        _mediaCommentViewModel = new MediaCommentViewModel(new MediaCommentDataManagerMock());
        ILogService.Singleton = new LogServiceMock();
    }

    [Fact, Trait("Category", "Unit")]
    public void TestCommentsListLoadedOnInitialEmpty()
    {
        Assert.Empty(_mediaCommentViewModel.GetComments(41, "MockId-1"));
    }

    [Fact, Trait("Category", "Unit")]
    public void TestAddCommentToMovie()
    {
        _mediaCommentViewModel.GetComments(41, "MockId-1");
        _mediaCommentViewModel.AddComment("Lorem Ipsum est");
        Assert.Single(_mediaCommentViewModel.GetComments(41, "MockId-1"));
    }

    [Fact, Trait("Category", "Unit")]
    public void TestAddMultipleComments()
    {
        _mediaCommentViewModel.GetComments(41, "MockId-1");
        _mediaCommentViewModel.AddComment("Lorem Ipsum est");
        System.Threading.Thread.Sleep(1000);
        _mediaCommentViewModel.AddComment("Ipsum Lorem est");
        Assert.Equal(2, _mediaCommentViewModel.GetComments(41, "MockId-1").Count);
        Assert.True(_mediaCommentViewModel.GetComments(41, "MockId-1").OrderByDescending(c => c.Created).First().CommentText.Equals("Ipsum Lorem est"));
    }

    [Fact, Trait("Category", "Unit")]
    public void TestUpdateComment()
    {
        _mediaCommentViewModel.GetComments(41, "MockId-1");
        _mediaCommentViewModel.AddComment("Lorem Ipsum est");
        int commentId = _mediaCommentViewModel.GetComments(41, "MockId-1").First().Id;
        _mediaCommentViewModel.UpdateComment(commentId, "Changed Text");
        Assert.True(_mediaCommentViewModel.GetComments(41, "MockId-1").First().CommentText.Equals("Changed Text"));
    }

    [Fact, Trait("Category", "Unit")]
    public void TestDeleteComment()
    {
        _mediaCommentViewModel.GetComments(41, "MockId-1");
        _mediaCommentViewModel.AddComment("Lorem Ipsum est");
        int commentId = _mediaCommentViewModel.GetComments(41, "MockId-1").First().Id;
        _mediaCommentViewModel.DeleteComment(commentId);
        Assert.DoesNotContain(_mediaCommentViewModel.GetComments(41, "MockId-1"), c => c.Id == commentId);
    }

    [Fact, Trait("Category", "Unit")]
    public void TestEditCommentOfOtherUserThrows()
    {
        _mediaCommentViewModel.GetComments(41, "MockId-1");
        _mediaCommentViewModel.AddComment("Lorem Ipsum est");
        int commentId = _mediaCommentViewModel.GetComments(41, "MockId-1").First().Id;
        _mediaCommentViewModel.GetComments(41, "MockId-2");
        Assert.Throws<InvalidOperationException>(() => _mediaCommentViewModel.UpdateComment(commentId, "Changed Text"));
    }

    [Fact, Trait("Category", "Unit")]
    public void TestDeleteCommentOfOtherUserThrows()
    {
        _mediaCommentViewModel.GetComments(41, "MockId-1");
        _mediaCommentViewModel.AddComment("Lorem Ipsum est");
        int commentId = _mediaCommentViewModel.GetComments(41, "MockId-1").First().Id;
        _mediaCommentViewModel.GetComments(41, "MockId-2");
        Assert.Throws<InvalidOperationException>(() => _mediaCommentViewModel.DeleteComment(commentId));
    }
}