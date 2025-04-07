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
}