// <copyright file="ZeroCountes.cs" company="AV-Bog">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Testing3;

/// <summary>
/// Provides functionality to count null or zero elements in a custom list
/// </summary>
public static class ZeroCountes
{
    /// <summary>
    /// Counts the number of null or zero elements in the list based on provided null checker
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="list">The list to check for null elements.</param>
    /// <param name="nullChecker">The checker that determines what is considered null for type T.</param>
    /// <returns>The count of null elements.</returns>
    /// <exception cref="ArgumentNullException">Thrown when list or nullChecker is null.</exception>
    public static int CountNulls<T>(MyList<T> list, INullChecker<T> nullChecker)
    {
        if (list == null)
        {
            throw new ArgumentNullException(nameof(list));
        }

        if (nullChecker == null)
        {
            throw new ArgumentNullException(nameof(nullChecker));
        }

        int count = 0;
        foreach (var item in list)
        {
            if (nullChecker.IsNull(item))
            {
                count++;
            }
        }

        return count;
    }

    /// <summary>
    /// Interface for checking if an item of type T is considered null
    /// </summary>
    /// <typeparam name="T">The type of item to check.</typeparam>
    public interface INullChecker<T>
    {
        /// <summary>
        /// Determines whether the specified item is considered null
        /// </summary>
        /// <param name="item">The item to check.</param>
        /// <returns>true if the item is considered null; otherwise, false.</returns>
        bool IsNull(T item);
    }
}
