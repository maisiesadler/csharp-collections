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
    public void CanHandleCollisions()
    {
        var set = new MyHashSet<int>();

        set.Add(1);

        Assert.False(set.Contains(9));
    }

    [Fact]
    public void CanAddNegativeItemToSet()
    {
        var set = new MyHashSet<int>();

        set.Add(-7);

        Assert.True(set.Contains(-7));
        Assert.False(set.Contains(7));
    }

    [Fact]
    public void CanAddStringsToSet()
    {
        var set = new MyHashSet<string>();

        set.Add("no");

        Assert.True(set.Contains("no"));
        Assert.False(set.Contains("potatoes"));
    }

    [Fact]
    public void DoObjectsMakeSenseAsKeys()
    {
        var set = new MyHashSet<MyObject>();

        var o1 = new MyObject();
        var o2 = new MyObject();

        set.Add(o1);

        Assert.True(set.Contains(o1));
        Assert.False(set.Contains(o2));
    }

    public class MyObject
    {
    }
}
