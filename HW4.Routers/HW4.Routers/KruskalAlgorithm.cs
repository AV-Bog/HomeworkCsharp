namespace HW4.Routers;

public class KruskalAlgorithm
{
    public static Graph FindMaximumSpanningTree(Graph graph)
    {
        Graph minimumSpanningTree = new Graph();
    
        List<Edge> edges = new List<Edge>(graph);
        edges.Sort((a, b) => b.EdgeWeight.CompareTo(a.EdgeWeight));
        
        List<Set> sets = new List<Set>();
        foreach (Edge edge in edges)
        {
            bool hasSetA = false;
            bool hasSetB = false;
            Set setA = null, setB = null;

            foreach (Set set in sets)
            {
                if (set.Contains(edge.VertexA))
                {
                    hasSetA = true;
                    setA = set;
                }

                if (set.Contains(edge.VertexB))
                {
                    hasSetB = true;
                    setB = set;
                }
            }

            if (!hasSetA && !hasSetB)
            {
                Set newSet = new Set(edge);
                sets.Add(newSet);
            }
                
            else if (!hasSetA)
            {
                setB.SetGraph.Add(edge);
                setB.Vertices.Add(edge.VertexA);
            }
            else if (!hasSetB)
            {
                setA.SetGraph.Add(edge);
                setA.Vertices.Add(edge.VertexB);
            }
            else if (setA != setB)
            {
                setA.Union(setB, edge);
                sets.Remove(setB);
            }
        }
        
        if (sets.Count > 0)
        {
            minimumSpanningTree = sets[0].SetGraph;
            for (int i = 1; i < sets.Count; i++)
            {
                minimumSpanningTree = sets[i].SetGraph;
            }
        }
        
        return minimumSpanningTree;
    }
}










