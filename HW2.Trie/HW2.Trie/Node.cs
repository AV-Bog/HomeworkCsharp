namespace HW2.Trie;

public class Node<T>
{
    public char Symbol { get; set; }
    public bool IsWord { get; set; }
    public Dictionary<char, Node<T>> SubNodes { get; set; }
    
    public Node(char symbol)
    {
        Symbol = symbol;
        SubNodes = new Dictionary<char, Node<T>>();
    }

    public Node<T> TryFind(char symbol) // пытается найти дочерний узел (Node<T>) в коллекции SubNodes по заданному символу (symbol)
    {
        if (SubNodes.TryGetValue(symbol, out Node<T> value))
        {
            return value;
        }
        else
        {
            return null;
        }
    }
}