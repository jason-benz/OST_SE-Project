namespace MediaHub.Data.FeedModule.ViewModel;

public class FilterBarViewModel : IFilterbarViewModel
{
    public FilterBarViewModel(Dictionary<string, bool> filterSettings)
    {
        FilterSettings = filterSettings;
    }

    public event FilterChangedCallback? OnFilterChanged;
    public void OnChange(string filterName)
    {
        FilterSettings[filterName] = !FilterSettings[filterName];
        OnFilterChanged?.Invoke(filterName, FilterSettings[filterName]);
    }

    public Dictionary<string, bool> FilterSettings { get;}
}