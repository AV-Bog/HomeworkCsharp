namespace HW3.LZW;

public class Node (byte symbol)
{
    private byte Symbol { get; } = symbol;
    private Dictionary<byte, Node> SubNodes { get; } = new();
    private int Code { get; set; } = -1;
    
    public Node? TryFind(byte symbol)
    {
        return SubNodes.TryGetValue(symbol, out var node) ? node : null;
    }
    
    public void AddNode(byte[] key, int code)
    {
        if (key.Length == 0)
        {
            Code = code;
        }
        else
        {
            byte symbol = key[0];
            if (!SubNodes.TryGetValue(symbol, out var subnode))
            {
                subnode = new Node(symbol);
                SubNodes.Add(symbol, subnode);
            }
            subnode.AddNode(key[1..], code);
        }
    }
    
    public bool TryGetCode(byte[] key, out int code)
    {
        if (key.Length == 0)
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