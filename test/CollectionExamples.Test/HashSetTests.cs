namespace CollectionExamples.Test;

public class HashSetTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(27)]
    [InlineData(109)]
    public void EmptySetDoesNotContainItem(int firstItem)
    {
        var set = new MyHashSet<int>();

        Assert.False(set.Contains(firstItem));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(8)]
    [InlineData(27)]
    [InlineData(109)]
    public void CanAddItemToSet(int firstItem)
    {
        var set = new MyHashSet<int>();

        set.Add(firstItem);

        Assert.True(set.Contains(firstItem));
    }

    [Fact]
    public void CanAddNegativeItemToSet()
    {
        var set = new MyHashSet<int>();

        set.Add(-7);

        Assert.True(set.Contains(-7));
        Assert.False(set.Contains(7));
    }
}
