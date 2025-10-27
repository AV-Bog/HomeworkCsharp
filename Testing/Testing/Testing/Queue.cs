using System.Collections.Concurrent;
using System.Data;
using System.Net.Quic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

namespace Testing;

/// <summary>
/// Represents a priority queue implemented as a binary heap.
/// Elements are dequeued based on their priority and insertion order.
/// </summary>
/// <typeparam name="T">The type of elements stored in the queue.</typeparam>
public class Queue<T>
{
    /// <summary>
    /// Represents an item in the priority queue with its value, priority, and timestamp.
    /// </summary>
    private struct QueueItem
    {
        /// <summary>
        /// The value of the item.
        /// </summary>
        public T Value { get; }
        /// <summary>
        /// The priority of the item. Higher values indicate higher priority.
        /// </summary>
        public int Priority { get; }
        /// <summary>
        /// The timestamp of when the item was added to the queue. Used for tie-breaking.
        /// </summary>
        public long Timestamp { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueueItem"/> struct.
        /// </summary>
        /// <param name="value">The value of the item.</param>
        /// <param name="priority">The priority of the item.</param>
        /// <param name="timestamp">The timestamp of the item.</param>
        public QueueItem(T value, int priority, long timestamp)
        {
            Value = value;
            Priority = priority;
            Timestamp = timestamp;
        }
    }

    private QueueItem[] heap;
    private int count;
    private long timestampCounter;
    
    public bool Empty => count == 0;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="Queue{T}"/> class with an optional initial capacity.
    /// </summary>
    /// <param name="initialCapacity">The initial capacity of the queue. Default is 4.</param>
    /// <exception cref="ArgumentException">Thrown when the initial capacity is less than 1.</exception>
    public Queue(int initialCapacity = 4) //если предается размер очереди или ничего не передается 
    {
        if (initialCapacity < 1)
        {
            throw new ArgumentException("Initial capacity must be at least 1.");
        }
        
        heap = new QueueItem[initialCapacity];
        count = 0;
        timestampCounter = 0;
    }
    
    /// <summary>
    /// Adds an element to the queue with the specified priority.
    /// </summary>
    /// <param name="value">The value to enqueue.</param>
    /// <param name="priority">The priority of the value. Higher values indicate higher priority.</param>
    public void Enqueue(T value, int priority)
    {
        if (count == heap.Length) //чтоб было куда добавлять
        {
            Resize(heap.Length * 2);
        }
        var item = new QueueItem(value, priority, timestampCounter++);
        heap[count] = item;
        Up(count);
        count++;
    }

    private void Up(int i)
    {
        while (i != 0 )
        {
            int parent = (i - 1) / 2;
            if (Compare(heap[i], heap[parent]) <= 0)
            {
                break;
            }
            Swap(i, parent);
            i = parent;
        }
    }

    private void Swap(int i, int j)
    {
        var temp = heap[i];
        heap[i] = heap[j];
        heap[j] = temp;
    }

    public T Dequeue()
    {
        if (count == 0)
        {
            throw new InvalidOperationException("Queue is empty");
        }
        
        var value = heap[0].Value;
        heap[0] = heap[count - 1];
        count--;
        Down(0);

        if (count < heap.Length / 4 && heap.Length > 4)
        {
            Resize(heap.Length / 2);
        }
        
        return value;
    }

    private void Down(int i)
    {
        while (2 * i + 1 < count)
        {
            int left = 2 * i + 1;
            int right = 2 * i + 2;
            int largest = i;
            
            if (left < count && Compare(heap[left], heap[largest]) > 0)
            {
                largest = left;
            }

            if (right < count && Compare(heap[right], heap[largest]) > 0)
            {
                largest = right;
            }

            if (largest == i)
            {
                break;
            }
            Swap(i, largest);
            i = largest;
        }
    }

    private int Compare(QueueItem item1, QueueItem item2)
    {
        int priorityCompare = item2.Priority.CompareTo(item1.Priority);
        if (priorityCompare != 0)
        {
            return priorityCompare;
        }
        return item2.Timestamp.CompareTo(item1.Timestamp);
    }
    
    private void Resize(int newSize)
    {
        var newArray = new QueueItem[newSize];
        Array.Copy(heap, newArray, count);
        heap = newArray;
    }
}