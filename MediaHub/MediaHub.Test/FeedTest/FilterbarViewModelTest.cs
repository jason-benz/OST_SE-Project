using System.Collections.Generic;
using MediaHub.Data.FeedModule.ViewModel;
using Xunit;

namespace MediaHub.Test.FeedTest;

public class FilterbarViewModelTest
{
    private IFilterbarViewModel _filterbarViewModel;
    public FilterbarViewModelTest()
    {
        var dict = new Dictionary<string, bool>();
        dict["testfilter1"] = true;
        dict["testfilter2"] = false;
        _filterbarViewModel = new FilterBarViewModel(dict);
    }

    [Fact, Trait("Category", "Unit")]
    public void TestFilterbarVmContainsFilters()
    {
        Assert.Equal(2, _filterbarViewModel.FilterSettings.Count);
    }
    
    [Fact, Trait("Category", "Unit")]
    public void TestCallbackIsCalled()
    {
        string filterName = "";
        _filterbarViewModel.OnFilterChanged += (string name, bool value) => { filterName = name;};
        _filterbarViewModel.OnChange("testfilter1");
        Assert.Equal("testfilter1", filterName);
    } 
    
    [Fact, Trait("Category", "Unit")]
    public void TestTrueBecomesFalse()
    {
        _filterbarViewModel.OnChange("testfilter1");
        Assert.False(_filterbarViewModel.FilterSettings["testfilter1"]);
    }
    
    [Fact, Trait("Category", "Unit")]
    public void TestFalseBecomesTrue()
    {
        _filterbarViewModel.OnChange("testfilter2");
        Assert.True(_filterbarViewModel.FilterSettings["testfilter2"]);
    }
}