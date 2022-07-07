namespace CollectionExamples;
public class MyList<T>
{
    private int _length = 0;
    private int[] _values = new int[8];

    public void Add(int number)
    {
        if (_length >= _values.Length)
        {
            var temp = _values;
            _values = new int[_length * 2];
            for (var i = 0; i < temp.Length; i++)
            {
                _values[i] = temp[i];
            }
        }

        _values[_length] = number;
        _length++;
    }

    public int Length => _length;

    public int this[int index]
    {
        get
        {
            return _values[index];
        }
    }
}
