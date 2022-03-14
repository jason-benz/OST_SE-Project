using Xunit;

namespace MediaHub.Test;

public class UnitTest1
{

    public UnitTest1()
    {
        //Do setup here: Instance is created for each and every test-function
    }
    
    [Fact]
    public void Test1()
    {
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public void Test2(int x)
    {
    }
    
    
}