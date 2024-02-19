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
    }
}
