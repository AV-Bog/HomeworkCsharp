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
    
    public void AddNode(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            IsWord = true;
        }
        else
        {
            var symbol = key[0];
            if (SubNodes.TryGetValue(symbol, out var subnode))
            {
                subnode?.AddNode(key[1..]);
            }
            else
            {
                var newNode = new Node<T>(symbol);
                SubNodes.Add(symbol, newNode);
                newNode.AddNode(key[1..]);
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
            
        bool result = RemoveNode(key[1..], subnode);

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
                return true;
            }
        }
        else
        {
            var subnode = node?.TryFind(key[0]);
            if (subnode != null)
            {
                result = SearchNode(key[1..], subnode);
            }
        }

        return result;
    }
    
    public static int CountingWordsWithThisPrefix(String prefix, Node<T>? node)
    {
        if (node == null)
        {
            return 0;
        }
        
        int result = 0;
        while (!string.IsNullOrEmpty(prefix))
        {
            node = node.TryFind(prefix[0]);
            prefix = prefix[1..];
        }
        
        if (node.IsWord)
        {
            ++result;
        }

        if (node.SubNodes.Count > 0)
        {
            foreach (var keyValuePair in node.SubNodes)
            {
                result += CountingWordsWithThisPrefix(prefix, keyValuePair.Value);
            }
        }
        
        return result;
    }
}
