using MediaHub.Data.Model;

namespace MediaHub.Data.ViewModel;

public interface IRatingViewModel
{
    public void Load(string userId, IMovie movie);
    public bool IsAddedToProfile { get; }
    public void ToggleIsAddedToProfile();
    public void SetRating(byte value);
}