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
    
    public bool RemoveNode(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            if (IsWord)
            {
                IsWord = false;
                return true;
            }
            return false;
        }
        
        var subnode = TryFind(key[0]);
        if (subnode == null)
        {
            return false;
        }
            
        bool result = subnode.RemoveNode(key[1..]);

        if (result)
        {
            if (subnode.SubNodes.Count == 0 && !subnode.IsWord)
            {
                SubNodes.Remove(key[0]);
            }
        }

        return result;
    }
    
    public bool SearchNode(string key)
    {
        var result = false;
        if (string.IsNullOrEmpty(key))
        {
            if (IsWord)
            {
                return true;
            }
        }
        else
        {
            var subnode = TryFind(key[0]);
            if (subnode != null)
            {
                result = subnode.SearchNode(key[1..]);
            }
        }

        return result;
    }
    
    public int CountingWordsWithThisPrefix(String prefix)
    {
        var currentNode = this;
        
        while (!string.IsNullOrEmpty(prefix))
        {
            currentNode = currentNode.TryFind(prefix[0]);
            if (currentNode == null)
            {
                return 0;
            }
            prefix = prefix[1..];
        }
        
        return CountWords(currentNode);
    }

    private int CountWords(Node<T> node)
    {
        int result = 0;

        if (node.IsWord)
        {
            ++result;
        }

        foreach (var subNode in node.SubNodes.Values)
        {
            result += CountWords(subNode);
        }
        return result;
    }
}
