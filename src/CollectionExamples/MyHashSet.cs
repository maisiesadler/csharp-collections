namespace CollectionExamples;
public class MyHashSet<T>
{
    private const int _size = 1000;
    private MyList<T>[] _values = new MyList<T>[_size];

    public void Add(T value)
    {
        var hash = GetHashCode(value);

        if (_values[hash] == null) _values[hash] = new MyList<T>();
        _values[hash].Add(value);
    }

    public bool Contains(T value)
    {
        var hash = GetHashCode(value);

        if (_values[hash] == null) return false;

        var values = _values[hash];
        return values.Contains(value);
    }

    private static int GetHashCode(T value)
    {
        if (value == null) throw new ArgumentNullException(nameof(value));

        var hash = value.GetHashCode();
        hash = Math.Abs(hash);
        hash = hash % _size;

        return hash;
    }
}
