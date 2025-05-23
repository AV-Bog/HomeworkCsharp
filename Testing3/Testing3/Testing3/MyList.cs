// <copyright file="MyList.cs" company="AV-Bog">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Testing3;

using System.Collections;

/// <summary>
/// Represents a custom generic list implementation
/// </summary>
/// <typeparam name="T">The type of elements in the list.</typeparam>
public class MyList<T> : IEnumerable<T>
{
    private T[] _list;
    private int _count;

    /// <summary>
    /// Initializes a new instance of the <see cref="MyList{T}"/> class that is empty.
    /// </summary>
    public MyList()
    {
        this._list = new T[4];
        this._count = 0;
    }

    /// <summary>
    /// Gets the number of elements contained in the list
    /// </summary>
    public int Count => this._count;

    /// <summary>
    /// Adds an item to the end of the list
    /// </summary>
    /// <param name="item">The item to add to the list.</param>
    public void Add(T item)
    {
        if (_count >= _list.Length)
        {
            Array.Resize(ref _list, _list.Length * 2);
        }
    
        _list[_count] = item;
        _count++;
    }

    /// <summary>
    /// Returns an enumerator that iterates through the list
    /// </summary>
    /// <returns>An enumerator that can be used to iterate through the list.</returns>
    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < this._count; i++)
        {
            yield return this._list[i];
        }
    }

    /// <summary>
    /// Returns an enumerator that iterates through the list
    /// </summary>
    /// <returns>An enumerator that can be used to iterate through the list.</returns>
    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

    /// <summary>
    /// Gets the element at the specified index
    /// </summary>
    /// <param name="index">The zero-based index of the element to get</param>
    /// <returns>The element at the specified index</returns>
    /// <exception cref="IndexOutOfRangeException">Thrown when index is less than 0 or greater than or equal to Count</exception>
    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= this._count)
            {
                throw new IndexOutOfRangeException();
            }

            return this._list[index];
        }
    }
}
