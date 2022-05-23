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
    public void AddComment(int mediaId, string userId, string text)
    {
        try
        {
            _mediaCommentDataManager.AddComment(mediaId, userId, text);
        } catch (Exception ex)
        {
            LogService.Singleton.LogException("An error occured while adding a Media Comment to the Database", LogService.LogCategory.Identity, ex);
            throw;
        }
    }

    public List<MediaComment> GetComments(int mediaId)
    {
        try
        {
            return _mediaCommentDataManager.LoadComments(mediaId);
        } catch (Exception ex)
        {
            LogService.Singleton.LogException("An unknown error occured while loading Media Comments from Database", LogService.LogCategory.Identity, ex);
            return new List<MediaComment>();
        }
    }

    public void UpdateComment(int Id, string userId, string text)
    {
        try
        {
            _mediaCommentDataManager.UpdateComment(Id, userId, text);
        } catch(Exception ex)
        {
            LogService.Singleton.LogException("An error occured while updating a Media Comments in Database", LogService.LogCategory.Identity, ex);
            throw;
        }
    }

    public void DeleteComment(int Id, string userId)
    {
        try
        {
            _mediaCommentDataManager.DeleteComment(Id, userId);
        }
        catch (Exception ex)
        {
            LogService.Singleton.LogException("An error occured while deleting a Media Comment from Database", LogService.LogCategory.Identity, ex);
            throw;
        }
    }
}