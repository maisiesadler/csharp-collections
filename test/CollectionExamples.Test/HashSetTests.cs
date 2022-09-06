namespace CollectionExamples.Test;

// public class HashableInt : IHashable
// {
//     private readonly int i;

//     public HashableInt(int i)
//     {
//         this.i = i;
//     }

//     public int GetBucket()
//     {
//         return this.i % 8;
//     }
// }

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
    public void EachReturnsEachItem()
    {
        var set = new MyHashSet<int>();

        set.Add(7);
        set.Add(42);
        set.Add(904);

        var all = set.Each().ToArray();

        Assert.Equal(3, all.Length);
        // Assert.Equal(7, all[0]);
        // Assert.Equal(42, all[1]);
        // Assert.Equal(904, all[2]);

        Assert.True(all.Contains(7));
    }

    // [Fact]
    // public void CanAddNegativeItemToSet()
    // {
    //     var set = new MyHashSet<WrappedInt>();

    //     set.Add(new WrappedInt(-7));

    //     Assert.True(set.Contains(new WrappedInt(-7)));
    //     Assert.False(set.Contains(new WrappedInt(7)));
    // }

    // [Fact]
    // public void CanAddStringsToSet()
    // {
    //     var set = new MyHashSet<string>();

    //     set.Add("no");

    //     Assert.True(set.Contains("no"));
    //     Assert.False(set.Contains("potatoes"));
    // }

    // [Fact]
    // public void DoObjectsMakeSenseAsKeys()
    // {
    //     var set = new MyHashSet<MyObject>();

    //     var o1 = new MyObject();
    //     var o2 = new MyObject();

    //     set.Add(o1);

    //     Assert.True(set.Contains(o1));
    //     Assert.False(set.Contains(o2));
    // }
}
