using System;
using System.Linq;
using MediaHub.Data.MediaModule.ViewModel;

using Xunit;

namespace MediaHub.Test.MediaCommentTest;

public class MediaCommentViewModelUnitTest
{
    private readonly IMediaCommentViewModel _mediaCommentViewModel;
    public MediaCommentViewModelUnitTest()
    {
        _mediaCommentViewModel = new MediaCommentViewModel(new MediaCommentDataManagerMock());
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
    public void TestTooLongCommentThrows()
    {
        string TooLongComment = get_unique_string(300);
        Assert.Throws<ArgumentOutOfRangeException>(() => _mediaCommentViewModel.AddComment(TooLongComment));
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

    private string get_unique_string(int string_length)
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var stringChars = new char[string_length];
        var random = new Random();

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        return new String(stringChars);
    }
}