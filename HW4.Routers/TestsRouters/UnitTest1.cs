using HW4.Routers;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace TestsRouters;

public class Tests
{
    [TestFixture]
    public class GraphTests
    {
        [Test]
        public void Edge_CompareTo_Null_Returns1()
        {
            var edge = new Edge(10, 1, 2);
            Assert.AreEqual(1, edge.CompareTo(null));
        }

        [Test]
        public void Edge_CompareTo_HigherWeight_ReturnsNegative1()
        {
            var edge1 = new Edge(10, 1, 2);
            var edge2 = new Edge(20, 1, 3);
            Assert.AreEqual(-1, edge1.CompareTo(edge2));
        }
    }
    
    [TestFixture]
    public class KruskalAlgorithmTests
    {
        [Test]
        public void FindMaximumSpanningTree_SingleEdge_ReturnsSameEdge()
        {
            var graph = new Graph();
            graph.Add(new Edge(10, 1, 2));
            
            var result = KruskalAlgorithm.FindMaximumSpanningTree(graph);
            
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(10, result.GetWeight());
        }

        [Test]
        public void FindMaximumSpanningTree_TwoEdgesNoCycle_ReturnsBothEdges()
        {
            var graph = new Graph();
            graph.Add(new Edge(10, 1, 2));
            graph.Add(new Edge(15, 2, 3));
            
            var result = KruskalAlgorithm.FindMaximumSpanningTree(graph);
            
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(25, result.GetWeight());
        }

        [Test]
        public void FindMaximumSpanningTree_ThreeEdgesWithCycle_ReturnsTwoEdges()
        {
            var graph = new Graph();
            graph.Add(new Edge(10, 1, 2));
            graph.Add(new Edge(20, 2, 3));
            graph.Add(new Edge(15, 1, 3));
            
            var result = KruskalAlgorithm.FindMaximumSpanningTree(graph);
            
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(35, result.GetWeight());
        }
    }

    [TestFixture]
    public class SetTests
    {
        [Test]
        public void Union_TwoSets_CombinesCorrectly()
        {
            var set1 = new Set(new Edge(10, 1, 2));
            var set2 = new Set(new Edge(15, 3, 4));
            
            set1.Union(set2, new Edge(20, 2, 3));
            
            Assert.AreEqual(4, set1.Vertices.Count);
            Assert.AreEqual(3, set1.SetGraph.Count());
            Assert.AreEqual(45, set1.SetGraph.GetWeight());
        }

        [Test]
        public void Contains_VertexInSet_ReturnsTrue()
        {
            var set = new Set(new Edge(10, 1, 2));
            Assert.IsTrue(set.Contains(1));
            Assert.IsTrue(set.Contains(2));
            Assert.IsFalse(set.Contains(3));
        }
    }
}