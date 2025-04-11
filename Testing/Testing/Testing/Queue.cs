using System.Collections.Concurrent;
using System.Data;
using System.Net.Quic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

namespace Testing;

public class Queue<T>
{
    private struct QueueItem
    {
        public T Value { get; }
        public int Priority { get; }
        public long Timestamp { get; }

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
    
    public Queue(int initialCapacity = 4) //если предается размер очереди или ничего не передается 
    {
        heap = new QueueItem[initialCapacity];
        count = 0;
        timestampCounter = 0;
    }
    
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
        return item1.Timestamp.CompareTo(item2.Timestamp);
    }
    
    private void Resize(int newSize)
    {
        var newArray = new QueueItem[newSize];
        Array.Copy(heap, newArray, count);
        heap = newArray;
    }
}