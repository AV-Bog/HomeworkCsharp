// <copyright file="GetPrimes.cs" author="AV-Bog">
// under MIT License
// </copyright>

using System.Collections;

namespace HW9.MyLinq;

public static class MyLinq
{
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

    public static IEnumerable Take<T>(this IEnumerable<T> seq, int n)
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

    public static IEnumerable Skip<T>(this IEnumerable<T> seq, int n)
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
    
    private static bool IsPrime(int number)
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
