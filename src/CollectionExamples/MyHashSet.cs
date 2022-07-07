namespace CollectionExamples;
public class MyHashSet<T>
{
    private bool[] _values = new bool[Int16.MaxValue];

    public void Add(T value)
    {
        if (value == null) throw new ArgumentNullException(nameof(value));

        var hash = value.GetHashCode();
        _values[hash] = true;
    }

    public bool Contains(T value)
    {
        if (value == null) throw new ArgumentNullException(nameof(value));

        var hash = value.GetHashCode();
        return _values[hash];
    }
}
