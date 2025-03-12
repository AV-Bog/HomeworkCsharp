namespace HW3.LZW;

public class Node (char symbol)
{
    private char Symbol { get; } = symbol;
    private Dictionary<char, Node> SubNodes { get; } = new();
    private int Code { get; set; } = -1;
    
    public Node? TryFind(char symbol)
    {
        return SubNodes.TryGetValue(symbol, out var node) ? node : null;
    }
    
    public void AddNode(string key, int code)
    {
        if (string.IsNullOrEmpty(key))
        {
            Code = code;
        }
        else
        {
            var symbol = key[0];
            if (!SubNodes.TryGetValue(symbol, out var subnode))
            {
                subnode = new Node(symbol);
                SubNodes.Add(symbol, subnode);
            }
            subnode.AddNode(key[1..], code);
        }
    }
    
    public bool TryGetCode(string key, out int code)
    {
        if (string.IsNullOrEmpty(key))
        {
            code = Code;
            return Code != -1;
        }
        
        var subnode = TryFind(key[0]);
        if (subnode == null)
        {
            code = -1;
            return false;
        }
        
        return subnode.TryGetCode(key[1..], out code);
    }
}