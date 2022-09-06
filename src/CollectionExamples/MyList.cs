namespace CollectionExamples;

public class MyList<T>
{
    private T[] _values = new T[8];
    private int _length = 0;

    public void Add(T value)
    {
        if (_length >= _values.Length)
        {
            var copy = new T[_length * 2];
            for (var i = 0; i < _values.Length; i++)
            {
                copy[i] = _values[i];
            }

            _values = copy;
        }

        _values[_length] = value;
        _length++;
    }

    public int Length => _length;

    public IEnumerable<T> Each()
    {
        for (var i = 0; i < _length; i++)
        {
            yield return _values[i];
        }
    }

    public bool Contains(T value)
    {
        for (var i = 0; i < _length; i++)
        {
            if (_values[i]?.Equals(value) == true)
                return true;
        }

        return false;
    }

    public T this[int index]
    {
        get
        {
            return _values[index];
        }
    }
}
