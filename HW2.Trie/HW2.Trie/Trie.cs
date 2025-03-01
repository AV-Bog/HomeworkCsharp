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

    public bool Add(string key)
    {
        if (!Contains(key))
        {
            Node<T>.AddNode(key, root); 
            ++Size;
            return true;
        }

        return false;
    }

    public bool Remove(string key)
    {
        bool result = Node<T>.RemoveNode(key, root);
        if (result)
        {
            --Size;
        }
        return result;
    }

    public bool Contains(string key)
    {
        return Node<T>.SearchNode(key, root);
    }

    public int HowManyStartsWithPrefix(String prefix)
    {
        return Node<T>.CountingWordsWithThisPrefix(prefix, root);
    }
}
