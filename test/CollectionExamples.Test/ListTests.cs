namespace CollectionExamples.Test;

public class ListTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(27)]
    [InlineData(109)]
    public void CanAddItemToList(int firstItem)
    {
        var list = new MyList<int>();

        list.Add(firstItem);

        Assert.Equal(1, list.Length);
        Assert.Equal(firstItem, list[0]);
    }

    [Fact]
    public void CanAddMultipleItems()
    {
        var list = new MyList<int>();

        list.Add(7);
        list.Add(42);

        Assert.Equal(2, list.Length);
        Assert.Equal(7, list[0]);
        Assert.Equal(42, list[1]);
    }

    [Theory]
    [InlineData(10)]
    [InlineData(100)]
    [InlineData(1000)]
    public void CanAddLongList(int itemCount)
    {
        var list = new MyList<int>();

        for (var i = 0; i < itemCount; i++)
        {
            list.Add(i);
        }

        Assert.Equal(itemCount, list.Length);
        for (var i = 0; i < itemCount; i++)
        {
            Assert.Equal(i, list[i]);
        }
    }

    [Fact]
    public void EachReturnsEachItem()
    {
        var list = new MyList<int>();

        list.Add(7);
        list.Add(42);
        list.Add(904);

        var all = list.Each().ToArray();

        Assert.Equal(3, all.Length);
        Assert.Equal(7, all[0]);
        Assert.Equal(42, all[1]);
        Assert.Equal(904, all[2]);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(-12)]
    public void ListDoesNotContain(int firstItem)
    {
        var list = new MyList<int>();

        list.Add(1);
        list.Add(8);
        list.Add(27);
        list.Add(109);

        Assert.False(list.Contains(firstItem));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(8)]
    [InlineData(27)]
    [InlineData(109)]
    public void ListContains(int firstItem)
    {
        var list = new MyList<int>();

        list.Add(1);
        list.Add(8);
        list.Add(27);
        list.Add(109);

        Assert.True(list.Contains(firstItem));
    }

    // [Fact]
    // public void ListOfString()
    // {
    //     var list = new MyList<string>();

    //     list.Add("I");
    //     list.Add("hope");
    //     list.Add("this");
    //     list.Add("works");

    //     Assert.Equal(4, list.Length);
    //     Assert.Equal("I", list[0]);
    //     Assert.Equal("hope", list[1]);
    //     Assert.Equal("this", list[2]);
    //     Assert.Equal("works", list[3]);
    // }
}
