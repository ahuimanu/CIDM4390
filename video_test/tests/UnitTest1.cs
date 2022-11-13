namespace tests;

using mylibrary;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        //Arrange
        MathLibrary ml = new MathLibrary();

        //Act
        int z = ml.TheAdder(0, 1);

        //Assert
        Assert.Equal(z, 1);

    }
}