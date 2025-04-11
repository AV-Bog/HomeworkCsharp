using System.Collections.Concurrent;
using System.Data;
using System.Net.Quic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

namespace Testing;

public class Queue
{
    private int[] a;
    private int count;

    public Queue(int initialCapacity = 4) //если предается размер очереди или ничего не передается 
    {
        a = new int[initialCapacity];
        count = 0;
    }

    public Queue(int[] values) //если сразу список закидываем
    {
        a = new int[values.Length];
        Array.Copy(values, a, values.Length);
        count = values.Length;

        for (int i = count - 1; i >= 0; i--)
        {
            Down(i);
        }
    }
    
    public void Enqueue(int value)
    {
        if (count == a.Length) //чтоб было куда добавлять
        {
            Resize(a.Length * 2);
        }
        a[count] = value;
        Up(count);
        count++;
    }

    private void Up(int i)
    {
        while (i != 0 && a[i] > a[(i - 1) / 2])
        {
            Swap(a[i], a[(i - 1) / 2]);
            i = (i - 1) / 2;
        }
    }

    private void Swap(int i, int j)
    {
        int temp = a[i];
        a[i] = a[j];
        a[j] = temp;
    }

    public int Dequeue()
    {
        if (count == 0)
        {
            throw new InvalidOperationException("Queue is empty");
        }
        
        int value = a[0];
        a[0] = a[count - 1];
        count--;
        Down(0);
        return value;
    }

    private void Down(int i)
    {
        while (2 * i + 1 < count)
        {
            int maxChild = 2 * i + 1;
            if (maxChild + 1 < count && a[maxChild] < a[maxChild + 1])
            {
                maxChild++;
            }

            if (a[i] >= a[maxChild])
            {
                break;
            }
            Swap(i, maxChild);
            i = maxChild;
        }
    }

    private void Resize(int newSize)
    {
        int[] newArray = new int[newSize];
        Array.Copy(a, newArray, count);
        a = newArray;
    }
}