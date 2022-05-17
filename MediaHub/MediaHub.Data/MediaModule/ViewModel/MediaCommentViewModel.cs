using MediaHub.Data.MediaModule.Model;
using MediaHub.Data.PersistencyLayer;

namespace MediaHub.Data.MediaModule.ViewModel;

public class MediaCommentViewModel : IMediaCommentViewModel
{
    private readonly IMediaCommentDataManager _mediaCommentDataManager;

    public MediaCommentViewModel(IMediaCommentDataManager mediaCommentDataManager)
    {
        _mediaCommentDataManager = mediaCommentDataManager;
    }
    public void AddComment(string text)
    {
        try
        {
            _mediaCommentDataManager.AddComment(text);
        } catch (Exception ex)
        {
            ILogService.Singleton.LogException("An error occured while adding a Media Comment to the Database", ILogService.LogCategory.Identity, ex);
            throw;
        }
    }

    public List<MediaComment> GetComments(int mediaId, string userId)
    {
        try
        {
            _mediaCommentDataManager.Load(mediaId, userId);
            return _mediaCommentDataManager.MediaComments;
        } catch (Exception ex)
        {
            ILogService.Singleton.LogException("An unknown error occured while loading Media Comments from Database", ILogService.LogCategory.Identity, ex);
            return new List<MediaComment>();
        }
    }

    public void UpdateComment(int Id, string text)
    {
        try
        {
            _mediaCommentDataManager.UpdateComment(Id, text);
        } catch(Exception ex)
        {
            ILogService.Singleton.LogException("An error occured while updating a Media Comments in Database", ILogService.LogCategory.Identity, ex);
            throw;
        }
    }

    public void DeleteComment(int Id)
    {
        try
        {
            _mediaCommentDataManager.DeleteComment(Id);
        }
        catch (Exception ex)
        {
            ILogService.Singleton.LogException("An error occured while deleting a Media Comment from Database", ILogService.LogCategory.Identity, ex);
            throw;
        }
    }
}