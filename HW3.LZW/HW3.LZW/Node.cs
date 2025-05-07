namespace HW3.LZW;

public class Node ()
{
    private byte Symbol { get; }
    private Dictionary<byte, Node> SubNodes { get; } = new();
    private int Code { get; set; }

    public Node(byte symbol) : this()
    {
         Symbol = symbol;
    }
    
    public Node(ushort code) : this(0)
    {
        Code = code;
    }
    
    public Node? TryFind(byte symbol)
    {
        return SubNodes.TryGetValue(symbol, out var node) ? node : null;
    }
    
    public void AddNode(byte[] key, int code)
    {
        Node current = this;
        foreach (var b in key)
        {
            if (!current.SubNodes.ContainsKey(b))
            {
                current.SubNodes[b] = new Node(0);
            }
            
            current = current.SubNodes[b];
        }
        current.Code = (ushort)code;
    }
    
    public bool TryGetCode(byte[] key, out int code)
    {
        Node current = this;
        foreach (byte b in key)
        {
            if (!current.SubNodes.TryGetValue(b, out current))
            {
                code = 0;
                return false;
            }
        }
        code = current.Code;
        return code != 0;
    }
}