namespace CollectionExamples;

public class MyHashSet<T>
{
    private MyList<T>[] _values = new MyList<T>[8];

    public bool Contains(T value)
    {
        var bucket = GetBucket(value);

        var list = _values[bucket];
        if (list == null) return false;

        return list.Contains(value);
    }

    public void Add(T value)
    {
        var bucket = GetBucket(value);

        if (_values[bucket] == null)
            _values[bucket] = new MyList<T>();

        _values[bucket].Add(value);
    }

    public IEnumerable<T> Each()
    {
        foreach (var list in _values)
        {
            if (list != null)
            {
                foreach (var value in list.Each())
                {
                    yield return value;
                }
            }
        }
    }

    private int GetBucket(T value)
    {
        var hashcode = value.GetHashCode();
        return hashcode % 8;
    }
}
