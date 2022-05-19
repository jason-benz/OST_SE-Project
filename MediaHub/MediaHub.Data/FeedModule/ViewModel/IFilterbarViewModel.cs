namespace MediaHub.Data.FeedModule.ViewModel;

public delegate void FilterChangedCallback(string filterName, bool filterValue); 
public interface IFilterbarViewModel
{
    public void OnChange(string filterName);
    
    public event FilterChangedCallback? OnFilterChanged;
    public Dictionary<string, bool> FilterSettings { get;}
}