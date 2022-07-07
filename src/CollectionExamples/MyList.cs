namespace CollectionExamples;
public class MyList<T>
{
    private int _length = 0;
    private T[] _values = new T[8];

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

    public bool Contains(T value)
    {
        foreach (var item in _values)
        {
            if (item == null && value == null)
            {
                return true;
            }
            if (item != null && item.Equals(value))
            {
                return true;
            }
        }

        return false;
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
