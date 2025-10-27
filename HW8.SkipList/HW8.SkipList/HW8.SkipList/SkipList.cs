﻿// <copyright file="SkipList.cs" company="PlaceholderCompany">

﻿// Copyright (c) PlaceholderCompany. All rights reserved.
﻿// </copyright>
namespace HW8.SkipList;

using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Represents a collection of objects that is maintained in sorted order using a skip list data structure
/// </summary>
/// <typeparam name="T">The type of elements in the skip list</typeparam>
public class SkipList<T> : IList<T>
{
    private class Node(T? value, int height)
    {
        public T? Value { get; set; } = value;

        public Node?[] Next { get; set; } = new Node[height];

        public int Height => this.Next.Length;
    }

    private const double Probability = 0.5;
    private const int MaxLevels = 4;
    private readonly Random _random = new();
    private int _count = 0;
    private Node _head;
    private int _version = 0;

    /// <summary>
    /// Initializes a new instance of the <see cref="SkipList{T}"/> class.
    /// </summary>
    public SkipList()
    {
        this._head = new Node(default, MaxLevels);
        for (int i = 0; i < MaxLevels; i++)
        {
            this._head.Next[i] = null;
        }

        this._count = 0;
    }

    /// <summary>
    /// Returns an enumerator that iterates through the skip list
    /// </summary>
    /// <returns>An enumerator for the skip list.</returns>
    public IEnumerator<T> GetEnumerator()
    {
        Node? current = this._head.Next[0];
        while (current != null)
        {
            if (current.Value != null)
            {
                yield return current.Value;
            }

            current = current.Next[0];
        }
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    /// <summary>
    /// Adds an item to the skip list in sorted order
    /// </summary>
    /// <param name="item">The object to add to the skip list.</param>
    /// <exception cref="ArgumentNullException">Thrown when item is null.</exception>
    public void Add(T? item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        Node? current = this._head;
        int level = this.ChooseRandomLevel();
        Node?[] update = new Node?[MaxLevels];
        Node newNode = new Node(item, level);

        for (var i = MaxLevels - 1; i >= 0; i--)
        {
            while (current?.Next[i] != null && this.Compare(current.Next[i].Value, item) < 0)
            {
                current = current.Next[i];
            }

            update[i] = current;
        }

        for (int i = 0; i < level; i++)
        {
            newNode.Next[i] = update[i]?.Next[i];
            update[i].Next[i] = newNode;
        }

        this._count++;
    }

    private int Compare(T? a, T? b)
    {
        if (a == null && b == null)
        {
            return 0;
        }

        if (a == null)
        {
            return -1;
        }

        if (b == null)
        {
            return 1;
        }

        if (a is IComparable comparable)
        {
            return comparable.CompareTo(b);
        }

        throw new InvalidOperationException($"Тип {typeof(T)} должен реализовывать IComparable<T>");
    }

    private int ChooseRandomLevel()
    {
        int level = 1;
        while (level < MaxLevels && this._random.NextDouble() < Probability)
        {
            level++;
        }

        return level;
    }

    /// <summary>
    /// Removes all items from the skip list
    /// </summary>
    public void Clear()
    {
        for (int i = 0; i < this._head.Next.Length; i++)
        {
            this._head.Next[i] = null;
        }

        this._count = 0;
        this._version++;
    }

    /// <summary>
    /// Determines whether the skip list contains a specific value
    /// </summary>
    /// <param name="item">The object to locate in the skip list.</param>
    /// <returns>true if item is found; otherwise, false.</returns>
    /// <exception cref="ArgumentNullException">Thrown when item is null.</exception>
    public bool Contains(T? item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        Node? current = this._head;
        for (var i = MaxLevels - 1; i >= 0; i--)
        {
            while (current != null && current.Next[i] != null && this.Compare(current.Next[i].Value, item) <= 0)
            {
                current = current.Next[i];
                if (current != null && EqualityComparer<T>.Default.Equals(current.Value, item))
                {
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Copies the elements of the skip list to an Array, starting at a particular Array index
    /// </summary>
    /// <param name="array">The one-dimensional Array that is the destination of the elements.</param>
    /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
    /// <exception cref="ArgumentNullException">Thrown when array is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when arrayIndex is less than 0.</exception>
    /// <exception cref="ArgumentException">Thrown when the number of elements is greater than available space.</exception>
    public void CopyTo(T?[]? array, int arrayIndex)
    {
        if (array == null)
        {
            throw new ArgumentNullException(nameof(array));
        }

        if (arrayIndex < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(arrayIndex));
        }

        if (array.Length - arrayIndex < this._count)
        {
            throw new ArgumentException("В целемом массиве мало мета");
        }

        Node? current = this._head.Next[0];
        while (current != null)
        {
            array[arrayIndex++] = current.Value;
            current = current.Next[0];
        }
    }

    /// <summary>
    /// Removes the first occurrence of a specific object from the skip list
    /// </summary>
    /// <param name="item">The object to remove from the skip list.</param>
    /// <returns>true if item was successfully removed; otherwise, false.</returns>
    /// <exception cref="ArgumentNullException">Thrown when item is null.</exception>
    public bool Remove(T? item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        Node? current = this._head;
        Node[] update = new Node[MaxLevels];
        bool found = false;

        for (var i = MaxLevels - 1; i >= 0; i--)
        {
            while (current.Next[i] != null && this.Compare(current.Next[i].Value, item) < 0)
            {
                current = current.Next[i];
            }

            update[i] = current;
        }

        if (current.Next[0] != null && EqualityComparer<T>.Default.Equals(current.Next[0].Value, item))
        {
            found = true;
            Node? nodeToRemove = current.Next[0];

            if (nodeToRemove != null)
            {
                for (var i = 0; i < nodeToRemove.Height; i++)
                {
                    if (update[i].Next[i] != nodeToRemove)
                    {
                        break;
                    }

                    update[i].Next[i] = nodeToRemove.Next[i];
                }
            }

            this._count--;
            this._version++;
        }

        return found;
    }

    /// <summary>
    /// Gets the number of elements contained in the skip list
    /// </summary>
    public int Count => _count;

    /// <summary>
    /// Gets a value indicating whether the skip list is read-only
    /// </summary>
    public bool IsReadOnly => false;

    /// <summary>
    /// Determines the index of a specific item in the skip list
    /// </summary>
    /// <param name="item">The object to locate in the skip list.</param>
    /// <returns>The index of item if found; otherwise, -1.</returns>
    /// <exception cref="ArgumentNullException">Thrown when item is null.</exception>
    public int IndexOf(T? item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        var index = 0;
        Node? current = this._head.Next[0];
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

    /// <summary>
    /// Not supported for skip list - throws NotSupportedException
    /// </summary>
    /// <param name="index">The zero-based index at which item should be inserted.</param>
    /// <param name="item">The object to insert.</param>
    /// <exception cref="NotSupportedException">Always thrown as operation is not supported.</exception>
    public void Insert(int index, T item)
    {
        throw new NotSupportedException("Insert by index is not supported in SkipList. Use Add method instead.");
    }

    /// <summary>
    /// Removes the item at the specified index
    /// </summary>
    /// <param name="index">The zero-based index of the item to remove.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when index is invalid.</exception>
    public void RemoveAt(int index)
    {
        if (index < 0 || index >= this._count)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        Node current = this._head.Next[0] ?? throw new InvalidOperationException("List is empty");
        for (int i = 0; i < index; i++)
        {
            current = current.Next[0] ?? throw new InvalidOperationException("Unexpected null node");
        }

        this.Remove(current.Value);
    }

    /// <summary>
    /// Gets or sets the element at the specified index
    /// </summary>
    /// <param name="index">The zero-based index of the element to get or set.</param>
    /// <returns>The element at the specified index.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when index is invalid.</exception>
    public T? this[int index]
    {
        get
        {
            if (index < 0 || index >= this._count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            Node current = this._head.Next[0]!;
            for (var i = 0; i < index; i++)
            {
                current = current.Next[0]!;
            }

            return current.Value;
        }

        set
        {
            if (index < 0 || index >= this._count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            Node? current = this._head.Next[0];
            for (var i = 0; i < index; i++)
            {
                current = current?.Next[0];
            }

            if (current != null)
            {
                current.Value = value;
            }

            this._version++;
        }
    }

    /// <summary>
    /// Copies the elements of the skip list to a new array, preserving their sorted order.
    /// </summary>
    /// <returns>An array containing all elements of the skip list in sorted order.</returns>
    public T?[] ToArray()
    {
        T?[] array = new T[this._count];
        Node? current = this._head.Next[0];
        int index = 0;
        while (current != null)
        {
            array[index++] = current.Value;
            current = current.Next[0];
        }

        return array;
    }
}
