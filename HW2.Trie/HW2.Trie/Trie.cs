namespace HW2.Trie;

public class Trie<T>
{
    private Node<T> root;
    public int Size { get; set; }

    public Trie()
    {
        root = new Node<T>('\0');
        Size = 0;
    }

    public void Add(string key)
    {
        if (!Contains(key))
        {
            AddNode(key, root); 
            ++Size;
        }
    }

    private void AddNode(string key, Node<T> node)
    {
        if (string.IsNullOrEmpty(key))
        {
            if (!node.IsWord)
            {
                node.IsWord = true;
            }
        }
        else
        {
            var symbol = key[0];
            var subnode = node.TryFind(symbol);
            if (subnode != null)
            {
                AddNode(key.Substring(1), subnode);
            }
            else
            {
                var newNode = new Node<T>(key[0]);
                node.SubNodes.Add(key[0], newNode);
                AddNode(key.Substring(1), newNode);
            }
        }

    }

    public bool Remove(string key)
    {
        bool result = RemoveNode(key, root);
        if (result)
        {
            --Size;
        }
        return result;
    }

    public bool RemoveNode(string key, Node<T> node)
    {
        if (string.IsNullOrEmpty(key))
        {
            if (node.IsWord)
            {
                node.IsWord = false;
                return true;
            }
            return false;
        }
        
        var subnode = node.TryFind(key[0]);
        if (subnode == null)
        {
            return false;
        }
            
        bool result = RemoveNode(key.Substring(1), subnode);

        if (result)
        {
            if (subnode.SubNodes.Count == 0 && !subnode.IsWord)
            {
                node.SubNodes.Remove(key[0]);
            }
        }

        return result;
    }

    public bool Contains(string key)
    {
        return SearchNode(key, root);
    }

    private bool SearchNode(string key, Node<T> node)
    {
        var result = false;
        if (string.IsNullOrEmpty(key))
        {
            if (node.IsWord)
            {
                result = true;
                return result;
            }
        }
        else
        {
            var subnode = node.TryFind(key[0]);
            if (subnode != null)
            {
                result = SearchNode(key.Substring(1), subnode);
            }
        }

        return result;
    }

    public int HowManyStartsWithPrefix(String prefix)
    {
        return CountingWordsWithThisPrefix(prefix, root);
    }

    private int CountingWordsWithThisPrefix(String prefix, Node<T> node)
    {
        int result = 0;
        while (!string.IsNullOrEmpty(prefix))
        {
            node = node.TryFind(prefix[0]);
            prefix = prefix.Substring(1);
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