// <copyright file="UnitTest1.cs" author="AV-Bog">
// under MIT License
// </copyright>

namespace TestRouters;

using HW4.Routers;

/// <summary>
/// Contains unit tests for the routers project.
/// </summary>
public class Tests
{
    /// <summary>
    /// Contains tests for the Edge class.
    /// </summary>
    [TestFixture]
    public class GraphTests
    {
        /// <summary>
        /// Tests that comparing an edge to null returns 1.
        /// </summary>
        [Test]
        public void Edge_CompareTo_Null_Returns1()
        {
            var edge = new Edge(10, 1, 2);
            Assert.That(edge.CompareTo(null), Is.EqualTo(1));
        }

        /// <summary>
        /// Tests that comparing edges with different weights returns correct result.
        /// </summary>
        [Test]
        public void Edge_CompareTo_HigherWeight_ReturnsNegative1()
        {
            var edge1 = new Edge(10, 1, 2);
            var edge2 = new Edge(20, 1, 3);
            Assert.That(edge1.CompareTo(edge2), Is.EqualTo(-1));
        }
    }

    /// <summary>
    /// Contains tests for the KruskalAlgorithm class.
    /// </summary>
    [TestFixture]
    public class KruskalAlgorithmTests
    {
        /// <summary>
        /// Tests that a graph with single edge returns the same edge as MST.
        /// </summary>
        [Test]
        public void FindMaximumSpanningTree_SingleEdge_ReturnsSameEdge()
        {
            var graph = new Graph();
            graph.Add(new Edge(10, 1, 2));

            var result = KruskalAlgorithm.FindMaximumSpanningTree(graph);

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.GetWeight(), Is.EqualTo(10));
        }

        /// <summary>
        /// Tests that a graph with two non-cyclic edges returns both edges in MST.
        /// </summary>
        [Test]
        public void FindMaximumSpanningTree_TwoEdgesNoCycle_ReturnsBothEdges()
        {
            var graph = new Graph();
            graph.Add(new Edge(10, 1, 2));
            graph.Add(new Edge(15, 2, 3));

            var result = KruskalAlgorithm.FindMaximumSpanningTree(graph);

            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.GetWeight(), Is.EqualTo(25));
        }

        /// <summary>
        /// Tests that a graph with cyclic edges returns correct MST.
        /// </summary>
        [Test]
        public void FindMaximumSpanningTree_ThreeEdgesWithCycle_ReturnsTwoEdges()
        {
            var graph = new Graph();
            graph.Add(new Edge(10, 1, 2));
            graph.Add(new Edge(20, 2, 3));
            graph.Add(new Edge(15, 1, 3));

            var result = KruskalAlgorithm.FindMaximumSpanningTree(graph);

            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.GetWeight(), Is.EqualTo(35));
        }
    }

    /// <summary>
    /// Contains tests for the Set class.
    /// </summary>
    [TestFixture]
    public class SetTests
    {
        /// <summary>
        /// Tests that union of two sets combines them correctly.
        /// </summary>
        [Test]
        public void Union_TwoSets_CombinesCorrectly()
        {
            var set1 = new Set(new Edge(10, 1, 2));
            var set2 = new Set(new Edge(15, 3, 4));

            set1.Union(set2, new Edge(20, 2, 3));

            Assert.That(set1.Vertices.Count, Is.EqualTo(4));
            Assert.That(set1.SetGraph.Count(), Is.EqualTo(3));
            Assert.That(set1.SetGraph.GetWeight(), Is.EqualTo(45));
        }

        /// <summary>
        /// Tests that Contains method correctly identifies vertices in set.
        /// </summary>
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