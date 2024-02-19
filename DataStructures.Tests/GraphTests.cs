using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructure.Graph;
using Xunit;

namespace DataStructure.Tests
{
    public class GraphTests
    {
        [Fact]
        public void Should_Create_Graph_And_Add_Vertex()
        {
            var g = new Graph<int>();

            Assert.NotNull(g);
            Assert.Equal(0, g.Vertexs);
            Assert.Equal(0, g.Edges);

            g.AddVertex(1);
            g.AddVertex(2);

            Assert.Equal(2, g.Vertexs);
        }

        [Fact]
        public void Should_Create_Graph_And_Add_Edges()
        {
            var g = new Graph<int>();

            Assert.NotNull(g);
            Assert.Equal(0, g.Vertexs);
            Assert.Equal(0, g.Edges);

            g.AddEdge(1, 2);
            g.AddEdge(2, 3);
            g.AddEdge(2, 4);
            g.AddEdge(3, 4);

            Assert.Equal(4, g.Vertexs);
            Assert.Equal(4, g.Edges);
        }

        [Fact]
        public void Should_Remove_Edge_And_Vertex() 
        { 
            var g = new Graph<int>();
            Assert.NotNull(g);

            g.AddEdge(1, 2);
            g.AddEdge(2, 3);
            g.AddEdge(2, 4);
            g.AddEdge(3, 4);
            g.AddEdge(4, 5);
            g.AddEdge(5, 6);

            Assert.Equal(6, g.Vertexs);
            Assert.Equal(6, g.Edges);

            g.RemoveVertex(6);
            Assert.Equal(5, g.Vertexs);
            Assert.Equal(5, g.Edges);

            g.RemoveEdge(3, 4);
            Assert.Equal(5, g.Vertexs);
            Assert.Equal(4, g.Edges);
        }

        [Fact]
        public void Should_Find_Node()
        {
            var g = new Graph<int>();
            Assert.NotNull(g);

            g.AddEdge(1, 2);
            g.AddEdge(2, 3);
            g.AddEdge(2, 4);
            g.AddEdge(3, 4);
            g.AddEdge(4, 5);
            g.AddEdge(5, 6);

            Assert.True(g.DFS(1, 6));
            Assert.True(g.BFS(1, 6));
        }

        [Fact]
        public void Should_Find_Shortest_Path_To_Vertex()
        {
            var g = new Graph<int>();
            Assert.NotNull(g);

            g.AddEdge(1, 2);
            g.AddEdge(1, 4);
            g.AddEdge(2, 3);
            g.AddEdge(2, 4);
            g.AddEdge(3, 4);
            g.AddEdge(4, 5);
            g.AddEdge(5, 6);

            var shortestPath = g.ShortestPath(1, 5);
            Assert.NotNull(shortestPath);
            Assert.NotEmpty(shortestPath);
            Assert.Equal(2, shortestPath.Length);
            Assert.Equal(new (int s, int d)[] { (1, 4), (4, 5) }, shortestPath);
        }
    }
}
