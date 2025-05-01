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
        throw new NotImplementedException();
    }

    public bool Contains(T item)
    {
        throw new NotImplementedException();
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public bool Remove(T item)
    {
        throw new NotImplementedException();
    }

    public int Count => _count;
    public bool IsReadOnly => false;
    public int IndexOf(T item)
    {
        throw new NotImplementedException();
    }

    public void Insert(int index, T item)
    {
        throw new NotImplementedException();
    }

    public void RemoveAt(int index)
    {
        throw new NotImplementedException();
    }

    public T this[int index]
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }
}