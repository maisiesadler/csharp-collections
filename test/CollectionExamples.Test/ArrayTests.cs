namespace CollectionExamples.Test;

public class ArrayTests
{
    [Fact]
    public void CreatingArrays()
    {
        Array array = Array.CreateInstance(typeof(int), 7);
        int[] intArray = (int[])array;

        intArray[0] = 14;

        Assert.Equal(7, intArray.Length);
        Assert.Equal(14, intArray[0]);
        Assert.Equal(0, intArray[1]);
    }

    [Fact]
    public void CreatingArrays_2()
    {
        var array = new int[7];

        array[0] = 14;

        Assert.Equal(7, array.Length);
        Assert.Equal(14, array[0]);
        Assert.Equal(0, array[1]);
    }

    [Fact]
    public void CreatingArrays_3()
    {
        var array = new int[] { 14, 0, 0, 0, 0, 0, 0 };

        Assert.Equal(7, array.Length);
        Assert.Equal(14, array[0]);
        Assert.Equal(0, array[1]);
    }
}
