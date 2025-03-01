namespace HW2.Trie;

public class Node<T> (char symbol)
{
    private char Symbol { get; } = symbol;
    private Dictionary<char, Node<T>?> SubNodes { get; } = new();
    private bool IsWord { get; set; }
    
    public Node<T>? TryFind(char symbol)
    {
        if (SubNodes.TryGetValue(symbol, out Node<T>? value))
        {
            return value;
        }
        else
        {
            return null;
        }
    }
    
    public static void AddNode(string key, Node<T>? node)
    {
        if (string.IsNullOrEmpty(key))
        {
            if (node != null) node.IsWord = true;
        }
        else
        {
            var symbol = key[0];
            var subnode = node?.TryFind(symbol);
            if (subnode != null)
            {
                AddNode(key[1..], subnode);
            }
            else
            {
                var newNode = new Node<T>(key[0]);
                node?.SubNodes.Add(key[0], newNode);
                AddNode(key.Substring(1), newNode);
            }
        }
    }
    
    public static bool RemoveNode(string key, Node<T>? node)
    {
        if (string.IsNullOrEmpty(key))
        {
            if (node != null && node.IsWord)
            {
                node.IsWord = false;
                return true;
            }
            return false;
        }
        
        var subnode = node?.TryFind(key[0]);
        if (subnode == null)
        {
            return false;
        }
            
        bool result = RemoveNode(key.Substring(1), subnode);

        if (result)
        {
            if (subnode.SubNodes.Count == 0 && !subnode.IsWord)
            {
                node?.SubNodes.Remove(key[0]);
            }
        }

        return result;
    }
    
    public static bool SearchNode(string key, Node<T>? node)
    {
        var result = false;
        if (string.IsNullOrEmpty(key))
        {
            if (node != null && node.IsWord)
            {
                result = true;
                return result;
            }
        }
        else
        {
            var subnode = node?.TryFind(key[0]);
            if (subnode != null)
            {
                result = SearchNode(key.Substring(1), subnode);
            }
        }

        return result;
    }
    
    public static int CountingWordsWithThisPrefix(String prefix, Node<T>? node)
    {
        int result = 0;
        while (!string.IsNullOrEmpty(prefix))
        {
            node = node?.TryFind(prefix[0]);
            prefix = prefix.Substring(1);
        }
        
        if (node != null && node.IsWord)
        {
            ++result;
        }

        if (node != null && node.SubNodes.Count > 0)
        {
            foreach (var keyValuePair in node.SubNodes)
            {
                result += CountingWordsWithThisPrefix(prefix, keyValuePair.Value);
            }
        }
        
        return result;
    }
}
