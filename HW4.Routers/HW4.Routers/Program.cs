using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using HW4.Routers;

if (args.Length != 2)
{
    Console.Error.WriteLine("Usage: <inputFile> <outputFile>");
    return 1;
}

try
{
    Graph graph = ReadGraphFromFile(args[0]);
    Graph mst = KruskalAlgorithm.FindMinimumSpanningTree(graph);

    if (!Graph.IsGraphConnected(mst))
    {
        Console.Error.WriteLine("Network is not connected");
        return 2;
    }

    WriteGraphToFile(mst, args[1]);
    return 0;
}
catch (Exception ex)
{
    Console.Error.WriteLine($"Error: {ex.Message}");
    return 3;
}

static Graph ReadGraphFromFile(string inputFile)
{
    Graph graph = new Graph();
    Dictionary<int, List<(int, int)>> routerConnections = new Dictionary<int, List<(int, int)>>();
    
    string[] lines = File.ReadAllLines(inputFile);
    foreach (var line in lines)
    {
        string[] parts = line.Split(':');
        if (parts.Length != 2) continue;
        
        int routerId = int.Parse(parts[0].Trim());
        string[] connections = parts[1].Split(',');

        foreach (var connection in connections)
        {
            string conn = connection.Trim();
            if (string.IsNullOrEmpty(conn))
            {
                continue;
            }
            
            int start = conn.IndexOf('(') + 1;
            int end = conn.IndexOf(')');
            int bandwidth = int.Parse(conn.Substring(start, end - start));
            int connectionId = int.Parse(conn.Substring(0, conn.IndexOf('(')).Trim());
            
            graph.Add(new Edge(bandwidth, routerId, connectionId));
        }
    }
    
    return graph;
}

static void WriteGraphToFile(Graph graph, string filePath)
{
    Dictionary<int, List<string>> routerConfigs = new Dictionary<int, List<string>>();

    foreach (Edge edge in graph)
    {
        if (!routerConfigs.ContainsKey(edge.VertexA))
            routerConfigs[edge.VertexA] = new List<string>();
        if (!routerConfigs.ContainsKey(edge.VertexB))
            routerConfigs[edge.VertexB] = new List<string>();

        if (edge.VertexA < edge.VertexB)
        {
            routerConfigs[edge.VertexA].Add($"{edge.VertexB} ({edge.EdgeWeight})");
        }
        else
        {
            routerConfigs[edge.VertexB].Add($"{edge.VertexA} ({edge.EdgeWeight})");
        }
    }

    using (StreamWriter writer = new StreamWriter(filePath))
    {
        foreach (var router in routerConfigs.Keys)
        {
            writer.Write($"{router}: ");
            writer.WriteLine(string.Join(", ", routerConfigs[router]));
        }
    }
}
