using MediaHub.Data.MediaModule.Model;

namespace MediaHub.Data.MediaModule.ViewModel;

public interface IRatingViewModel
{
    public void Load(string userId, Movie movie);
    public bool IsAddedToProfile { get; }
    public void ToggleIsAddedToProfile();
    public byte Rating { get; set; }
}