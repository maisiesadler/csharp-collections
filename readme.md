# Build your own data structures in C#

Why would we pick on collection over another and what do they look like under the hood?

This article explores some common C# types and how they could be built.

Build collections using only Arrays.

Look at Array, List and HashSet with Add, Each and Contains.

- Add = append a new item to the collection
- Each = return an iterator of every item in the collection
- Contains = determine if a given item is in the collection

## Array

We won't build our own array, but let's explore c# arrays.

What is an array?

_Structure of a fixed size holding items of the same type_

Operations
- Look items up by index
- Traverse the array

The size of arrays is set on creation.

To create an array of length 7 where each item is the default integer value, 0.

```csharp
var array = new int[7];
// array: [ 0, 0, 0, 0, 0, 0 ]
```

It supports setting a value in the array

```csharp
array[2] = 5;
// array: [ 0, 0, 5, 0, 0, 0 ]
```

Getting the value by index

```csharp
var x = array[2]; // x is 5
```

And searching for a value by traversing the collection

```csharp
var containsFive = false;
for (var i = 0; i < array.Length; i++)
{
    if (array[i] == 5) containsFive = true;
}

// containsFive is true
```

This can be simplified using the Linq method .Contains to

```csharp
var containsFive = array.Contains(5); // containsFive is true
```

<> But what if you want to add another item to the collection?

This is where lists come in.

## List

In C# `List` is a resizable array.
The functionality is the same as arrays, with a new method `Add`.

```csharp
var list = new List<int>();
// list.Length is 0

list.Add(45);
// list.Length is 1
```

What would this look like if we were to write it ourselves?
Let's try creating a new class `MyList`, and write a test for the functionality we want.

```csharp
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
```

Since we are taking a ([TDD approach](https://www.maisiesadler.co.uk/2021/10/06/tdd.html)) we should add only the functionality we need to make the test pass.

```csharp
public class MyList<T>
{
    private T[] _values = new T[8];

    public void Add(T value)
    {
        _values[0] = value;
    }

    public int Length => 1;

    public T this[int index]
    {
        get
        {
            return _values[index];
        }
    }
}
```

In the test we are only ever adding one item, we assume that `Add` always sets the first item and that Length is 1.

We know this isn't good enough and so add another test to add multiple items.

```csharp
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
```

To make this test pass we must add a new variable `_length`, every time we add a new item to the list the value increments.

```csharp
public class MyList<T>
{
    private int _length = 0;
    private T[] _values = new T[8];

    public void Add(T value)
    {
        _values[_length] = value;
        _length++;
    }

    public int Length => _length;

    public T this[int index]
    {
        get
        {
            return _values[index];
        }
    }
}
```

This makes the test pass, great, but what if we add 10 items to the list?

```csharp
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
```

Running ths test shows `System.IndexOutOfRangeException : Index was outside the bounds of the array.`.

Oh no, we need a bigger array! We could start with a bigger array, but that'd likely be overkill for most use cases.
Instead what we can do is copy all the items over to a new array when we see we are out of space.

The Add method of MyList becomes

```csharp
public void Add(T value)
{
    if (_length >= _values.Length)
    {
        var temp = _values;
        _values = new T[_length * 2];
        for (var i = 0; i < temp.Length; i++)
        {
            _values[i] = temp[i];
        }
    }

    _values[_length] = value;
    _length++;
}
```

The tests pass again, hurrah!

<add contains for list here?>

### Searching

What if we wanted to search for a value?

We can start with a test that checks that items do not contain in the list.

```csharp
[Theory]
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
```

Which is easy to implement

```csharp
public class MyList<T>
{
    public bool Contains(T value)
    {
        return false; 
    }
}
```

Then add the opposite test, checking that an item in the list can be found

```csharp
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
```

To know if an item is in the list we must loop through the whole list, if we find the item we can return true.

```csharp
public bool Contains(T value)
{
    foreach (var item in _values)
    {
        if (item.Equals(value))
        {
            return true;
        }
    }

    return false;
}
```

If you are familiar with [Big O Notation](https://rob-bell.net/2009/06/a-beginners-guide-to-big-o-notation) this is an O(n) operation meaning that it takes longer as the list length increases.

This could be ok, however sometimes we have to compare two lists to see if one list contains something from the other. In this case we have to loop through each list making it O(n * m) or O(n^2).

HashSets or maps are a collection that let us look up values with O(1) complexity.

## HashSet

HashSets are optimised for deciding if a value is in the set or not. The tests will focus on one operation `Contains`.

The first test checks that items don't belong in a newly created set.

```csharp
[Theory]
[InlineData(1)]
[InlineData(27)]
[InlineData(109)]
public void EmptySetDoesNotContainItem(int firstItem)
{
    var set = new MyHashSet<int>();

    Assert.False(set.Contains(firstItem));
}
```

The implementation is the the same as the list, simple but I fear it may get more complicated from here.

```csharp
public class MyHashSet<T>
{
    public bool Contains(T value)
    {
        return false; 
    }
}
```

The next test checks that items added to the HashSet can be found when calling `Contains`

```csharp
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
```

We want to be able to tell whether the value exists in the collection with O(1) complexity using arrays.
If the type we are using is `int` then we could use a `bool[]` and set the flag to true if the item is contained in the set.

```csharp
var hashset = new bool[8];

hashset[3] = true; // set contains 3

var containsOne = hashset[1]; // false
var containsThree = hashset[3]; // true
```

In our case we want to support multiple types and so we define a new operator `GetHashCode` that converts the type into an integer between 0 and the length of the array we will use. This then decides which "bucket" in the array the object relates to.

```csharp
private static int GetHashCode(T value)
{
    if (value == null) throw new ArgumentNullException(nameof(value));

    var hash = value.GetHashCode();
    hash = hash % 8;

    return hash;
}
```

This implementation uses the `GetHashCode` method defined on System.Object and restricts it to the length of the array.

With our method to convert objects to integers in place we can now implement the Add and Contains methods on `MyHashSet` backed by an array of bool.

```csharp
public class MyHashSet<T>
{
    private bool[] _values = new bool[8];

    public void Add(T value)
    {
        var hashCode = GetHashCode(value);
        _values[hashCode] = true;
    }

    public bool Contains(T value)
    {
        var hashCode = GetHashCode(value);
        return _values[hashCode];
    }

    private static int GetHashCode(T value) { ... }
}
```

There is an obvious flaw in this implementation since all the possible values are being reduced into 8 buckets, adding an item to a bucket will now return true for any value that is calculated to the bucket.

The following test fails.

```csharp
[Fact]
public void CanHandleCollisions()
{
    var set = new MyHashSet<int>();

    set.Add(1);

    Assert.False(set.Contains(9));
}
```

Values that return the same result for `GetHashCode` are called _collisions_.

One way around this is to use an array of lists instead of an array of bools in the HashSet.

```csharp
private MyList<T>[] _values = new MyList<T>[8];
```

When you add an item to the list instead of switching the bool from false to true you can add an item to a list.

```csharp
public void Add(T value)
{
    var hash = GetHashCode(value);

    if (_values[hash] == null) _values[hash] = new MyList<T>();
    _values[hash].Add(value);
}
```

Checking if the value exists using `Contains` then becomes looking for the correct bucket and then checking all the items in the list.

```csharp
public bool Contains(T value)
{
    var hash = GetHashCode(value);

    if (_values[hash] == null) return false;

    var values = _values[hash];
    return values.Contains(value);
}
```

There is still a lot to consider, for example, what should be done about negative numbers?

<dictionaries?>

## Conclusion

<>

Which collection should I use? Hopefully I have shown that it depends on your use case - if you want a collection of objects to loop through then `List<T>` is a good choice or else if you want to lookup values then `HashSet<T>` might be better suited.

Thanks for reading!

## Further reading
- https://towardsdatascience.com/8-common-data-structures-every-programmer-must-know-171acf6a1a42
