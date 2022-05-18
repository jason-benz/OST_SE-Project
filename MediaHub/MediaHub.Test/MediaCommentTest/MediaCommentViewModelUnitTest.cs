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
    private int _movieId = 41;
    private string _mockUserId1 = "MockId-1";
    private string _mockUserId2 = "MockId-2";
    public MediaCommentViewModelUnitTest()
    {
        _mediaCommentViewModel = new MediaCommentViewModel(new MediaCommentDataManagerMock());
        ILogService.Singleton = new LogServiceMock();
    }

    [Fact, Trait("Category", "Unit")]
    public void TestCommentsListLoadedOnInitialEmpty()
    {
        Assert.Empty(_mediaCommentViewModel.GetComments(_movieId));
    }

    [Fact, Trait("Category", "Unit")]
    public void TestAddCommentToMovie()
    {
        _mediaCommentViewModel.AddComment(_movieId, _mockUserId1, "Lorem Ipsum est");
        Assert.Single(_mediaCommentViewModel.GetComments(_movieId));
    }

    [Fact, Trait("Category", "Unit")]
    public void TestAddMultipleComments()
    {
        _mediaCommentViewModel.AddComment(_movieId, _mockUserId1, "Lorem Ipsum est");
        System.Threading.Thread.Sleep(1000);
        _mediaCommentViewModel.AddComment(_movieId, _mockUserId2, "Ipsum Lorem est");
        Assert.Equal(2, _mediaCommentViewModel.GetComments(_movieId).Count);
        Assert.True(_mediaCommentViewModel.GetComments(_movieId).OrderByDescending(c => c.Created).First().UserId.Equals(_mockUserId2));
    }

    [Fact, Trait("Category", "Unit")]
    public void TestUpdateComment()
    {
        _mediaCommentViewModel.AddComment(_movieId, _mockUserId1, "Lorem Ipsum est");
        int commentId = _mediaCommentViewModel.GetComments(_movieId).First(c=>c.UserId == _mockUserId1).Id;
        _mediaCommentViewModel.UpdateComment(commentId, _mockUserId1, "Changed Text");
        Assert.True(_mediaCommentViewModel.GetComments(_movieId).First(c => c.UserId == _mockUserId1).CommentText.Equals("Changed Text"));
    }

    [Fact, Trait("Category", "Unit")]
    public void TestDeleteComment()
    {
        _mediaCommentViewModel.AddComment(_movieId, _mockUserId1, "Lorem Ipsum est");
        int commentId = _mediaCommentViewModel.GetComments(_movieId).First(c => c.UserId == _mockUserId1).Id;
        _mediaCommentViewModel.DeleteComment(commentId, _mockUserId1);
        Assert.DoesNotContain(_mediaCommentViewModel.GetComments(_movieId), c => c.Id == commentId);
    }

    [Fact, Trait("Category", "Unit")]
    public void TestEditCommentOfOtherUserThrows()
    {
        _mediaCommentViewModel.AddComment(_movieId, _mockUserId1, "Lorem Ipsum est");
        int commentId = _mediaCommentViewModel.GetComments(_movieId).First(c => c.UserId == _mockUserId1).Id;
        Assert.Throws<InvalidOperationException>(() => _mediaCommentViewModel.UpdateComment(commentId, _mockUserId2, "Changed Text"));
    }

    [Fact, Trait("Category", "Unit")]
    public void TestDeleteCommentOfOtherUserThrows()
    {
        _mediaCommentViewModel.AddComment(_movieId, _mockUserId1, "Lorem Ipsum est");
        int commentId = _mediaCommentViewModel.GetComments(_movieId).First(c => c.UserId == _mockUserId1).Id;
        Assert.Throws<InvalidOperationException>(() => _mediaCommentViewModel.DeleteComment(commentId, _mockUserId2));
    }
}