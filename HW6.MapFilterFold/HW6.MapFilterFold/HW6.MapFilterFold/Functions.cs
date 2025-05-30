namespace HW6.MapFilterFold;

public static class Functions
{
    /// <summary>
    /// Transforms each list element using the provided mapper function
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown when list or mapper is null</exception>
    public static List<TResult> Map<TInput, TResult>(
        List<TInput> list,
        Func<TInput, TResult> mapper)
    {
        if (list == null)
            throw new ArgumentNullException(nameof(list));
        if (mapper == null)
            throw new ArgumentNullException(nameof(mapper));
    
        List<TResult> result = new List<TResult>();
        foreach (var item in list)
        {
            result.Add(mapper(item));
        }
        return result;
    }

    /// <summary>
    /// Filters elements based on the provided predicate
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown when list or filter is null</exception>
    public static List<TInput> Filter<TInput>(
        List<TInput> list,
        Func<TInput, bool> filter)
    {
        if (list == null)
            throw new ArgumentNullException(nameof(list));
        if (filter == null)
            throw new ArgumentNullException(nameof(filter));
    
        List<TInput> result = new List<TInput>();
        foreach (var item in list)
        {
            if (filter(item))
            {
                result.Add(item);
            }
        }
        return result;
    }

    /// <summary>
    /// Reduces the list to a single value using initial element and accumulator function
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown when list or func is null</exception>
    public static TAccumulator Fold<TInput, TAccumulator>(
        List<TInput> list, 
        TAccumulator element, 
        Func<TAccumulator, TInput, TAccumulator> func)
    {
        if (list == null) throw new ArgumentNullException(nameof(list));
        if (func == null) throw new ArgumentNullException(nameof(func));
        
        var acc = element;
        foreach (var item in list)
        {
            acc = func(acc, item);
        }
        
        return acc;
    }
}