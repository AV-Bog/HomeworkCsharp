using System.Collections;
using System.Collections.Generic;

namespace HW8.SkipList;

public class SkipList<T> : IList<T>
{
    private class Node
    {
        public T Value { get; set; }
        public Node[] Next { get; set; }
        
        public int Height => Next.Length;

        public Node(T value, int height)
        {
            Value = value;
            Next = new Node[height];
        }
    }
    
    private const double Probability = 0.5;
    private const int MaxLevels = 4;
    private readonly Random _random = new();
    private int _count = 0;
    private Node _head;
    private int _version = 0;
    
    public SkipList()
    {
        _head = new Node(default, MaxLevels);
    }
    
    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(T item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }
        
        var current = _head;
        int level = ChooseRandomLevel();
        var update = new Node[MaxLevels];
        var newNode = new Node(item, level);
        
        for (var i = MaxLevels - 1; i >= 0; i--)
        {
            while (current.Next != null && Compare(current.Next[i].Value, item) < 0)
            {
                current = current.Next[i];
            }
            update[i] = current;
        }

        for (int i = 0; i < level; i++)
        {
            newNode.Next[i] = update[i].Next[i];
            update[i].Next[i] = newNode;
        }
        
        _count++;
    }

    private int Compare(T a, T b)
    {
        if (a == null && b == null) return 0;
        if (a == null) return -1;
        if (b == null) return 1;

        if (a is IComparable comparable)
        {
            return comparable.CompareTo(b);
        }
        
        throw new InvalidOperationException($"Тип {typeof(T)} должен реализовывать IComparable<T>");
    }

    private int ChooseRandomLevel()
    {
        int level = 1;
        while (level < MaxLevels && _random.NextDouble() < Probability)
        {
            level++;
        }
        return level;
    }

    public void Clear()
    {
        for (int i = 0; i < _head.Next.Length; i++)
        {
            _head.Next[i] = null;
        }
        _count = 0;
        _version++;
    }

    public bool Contains(T item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }
        
        var current = _head;
        for (var i = MaxLevels - 1; i >= 0; i--)
        {
            while (current.Next[i] != null && Compare(current.Next[i].Value, item) <= 0)
            {
                current = current.Next[i];
                if (EqualityComparer<T>.Default.Equals(current.Value, item))
                {
                    return true;
                }
            }
        }
        
        return false;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        if (array == null)
        {
            throw new ArgumentNullException(nameof(array));
        }

        if (arrayIndex < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(arrayIndex));
        }

        if (array.Length - arrayIndex < _count)
        {
            throw new ArgumentException("В целемом массиве мало мета");
        }
        
        var current = _head.Next[0];
        while (current != null)
        {
            array[arrayIndex++] = current.Value;
            current = current.Next[0];
        }
    }

    public bool Remove(T item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        var current = _head;
        var update = new Node[MaxLevels];
        bool found = false;

        for (var i = MaxLevels - 1; i >= 0; i--)
        {
            while (current.Next[i] != null && Compare(current.Next[i].Value, item) < 0)
            {
                current = current.Next[i];
            }
            update[i] = current;
        }

        if (current.Next[0] != null && EqualityComparer<T>.Default.Equals(current.Next[0].Value, item))
        {
            found = true;
            var nodeToRemove = current.Next[0];

            for (var i = 0; i < nodeToRemove.Height; i++)
            {
                if (update[i].Next[i] != nodeToRemove) break;
                update[i] = nodeToRemove.Next[i];
            }

            _count--;
            _version++;
        }
        
        return found;
    }

    public int Count => _count;
    public bool IsReadOnly => false;
    public int IndexOf(T item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        var index = 0;
        var current = _head.Next[0];
        while (current != null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Value, item))
            {
                return index;
            }

            index++;
            current = current.Next[0];
        }
        return -1;
    }

    public void Insert(int index, T item)
    {
        throw new NotSupportedException("Insert by index is not supported in SkipList. Use Add method instead.");
    }

    public void RemoveAt(int index)
    {
        if (index < 0 || index >= _count)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        var current = _head.Next[0];
        Node previous = null;
        for (int i = 0; i < index; i++)
        {
            previous = current;
            current = current.Next[0];
        }

        Remove(current.Value);
    }

    public T this[int index]
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    
}

