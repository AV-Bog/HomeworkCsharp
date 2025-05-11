// <copyright file="MyLinq.cs" author="AV-Bog">
// under MIT License
// </copyright>

using System.Collections;

namespace HW9.MyLinq;

/// <summary>
/// Provides custom LINQ-like extension methods without using standard library implementations.
/// All methods are lazy-evaluated to prevent immediate materialization of sequences.
/// </summary>
public static class MyLinq
{
    /// <summary>
    /// Generates an infinite sequence of prime numbers using lazy evaluation.
    /// The sequence starts from 2 (the first prime number) and continues indefinitely.
    /// </summary>
    /// <returns>An infinite IEnumerable&lt;int&gt; sequence of prime numbers.</returns>
    public static IEnumerable<int> GetPrimes()
    {
        for (int i = 2;; i++)
        {
            if (IsPrime(i))
            {
                yield return i;
            }
        }
    }

    /// <summary>
    /// Returns a specified number of contiguous elements from the start of a sequence.
    /// </summary>
    /// <param name="seq">The sequence to take elements from.</param>
    /// <param name="n">The number of elements to take.</param>
    /// <typeparam name="T">The type of elements in the sequence.</typeparam>
    /// <returns>An IEnumerable&lt;T&gt; that contains the specified number of elements from the start of the input sequence.</returns>
    public static IEnumerable<T> TakeFirst<T>(this IEnumerable<T> seq, int n)
    {
        var count = 0;
        foreach (var item in seq)
        {
            if (count < n)
            {
                yield return item;
            }
            count++;
        }
    }

    /// <summary>
    /// Bypasses a specified number of elements in a sequence and then returns the remaining elements.
    /// </summary>
    /// <param name="seq">The sequence to skip elements from.</param>
    /// <param name="n">The number of elements to skip.</param>
    /// <typeparam name="T">The type of elements in the sequence.</typeparam>
    /// <returns>An IEnumerable&lt;T&gt; that contains the elements after the specified skip position.</returns>
    public static IEnumerable<T> MySkip<T>(this IEnumerable<T> seq, int n)
    {
        var count = 0;
        foreach (var item in seq)
        {
            if (count > n)
            {
                yield return item;
            }
            count++;
        }
    }
    
    public static bool IsPrime(int number)
    {
        if (number < 2)
        {
            return false;
        }

        if (number <= 3)
        {
            return true;
        }

        if (number % 2 == 0 || number % 3 == 0)
        {
            return false;
        }

        for (int i = 5; i * i < number; i += 6)
        {
            if (number % i == 0 || number % (i + 2) == 0)
            {
                return false;
            }
        }
        
        return true;
    }
}