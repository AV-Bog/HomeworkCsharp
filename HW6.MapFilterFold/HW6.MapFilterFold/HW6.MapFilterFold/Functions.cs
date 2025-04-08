namespace HW6.MapFilterFold;

public class Functions
{
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

    public static TAcc Fold<TInput, TAcc>(
        List<TInput> list, 
        TAcc element, 
        Func<TAcc, TInput, TAcc> func)
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