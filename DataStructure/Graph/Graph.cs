using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure.Graph
{
    public class Graph<T>
    {
        private readonly Dictionary<T, HashSet<T>> adjacencyList = new();
        public int Vertexs { get; private set; } = 0;
        public int Edges { get; private set; } = 0;

        public void AddVertex(T s)
        {
            adjacencyList.Add(s, new HashSet<T>());
            Vertexs++;
        }

        public void AddEdge(T source, T destination, bool bidirectional = true)
        {
            if (!adjacencyList.ContainsKey(source))
                AddVertex(source);

            if (!adjacencyList.ContainsKey(destination))
                AddVertex(destination);

            adjacencyList[source].Add(destination);
            if (bidirectional)
                adjacencyList[destination].Add(source);

            Edges++;
        }

        public bool RemoveEdge(T source, T destination)
        {
            var removed = false;

            if (adjacencyList.TryGetValue(source, out var sourceList))
            {
                removed |= sourceList.Remove(destination);
            }

            if (adjacencyList.TryGetValue(destination, out var destList))
            {
                removed = destList.Remove(source);
            }

            if (removed)
                Edges--;

            return removed;
        }

        public bool RemoveVertex(T vertex)
        {
            var removed = false;

            if(adjacencyList.ContainsKey(vertex))
                removed |= adjacencyList.Remove(vertex);

            foreach (var v in adjacencyList)
            {
                v.Value.Remove(vertex);
            }

            if(removed)
                Vertexs--;

            return removed;
        }

        public void Clear()
        {
            adjacencyList.Clear();
        }

        private bool DFS(T start, T goal, HashSet<T> visited)
        {
            visited.Add(start);

            var destinations = adjacencyList[start];
            foreach (var destination in destinations)
            {
                if (destination.Equals(goal))
                {
                    return true;
                }

                if (!visited.Contains(destination))
                {
                    return DFS(destination, goal, visited);
                }
            }

            return false;
        }

        public bool DFS(T start, T goal)
        {
            return DFS(start, goal, new HashSet<T>());
        }

        public bool BFS(T start, T goal)
        {
            var queue = new Queue<T>();
            queue.Enqueue(start);

            var visited = new HashSet<T>
            {
                start
            };

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                var destinations = adjacencyList[node];

                foreach (var destination in destinations)
                {
                    if (destination.Equals(goal))
                        return true;

                    if (!visited.Contains(destination))
                    {
                        queue.Enqueue(destination);
                        visited.Add(destination);
                    }
                }
            }

            return false;
        }

        private bool DistanceBFS(T src, T goal, Dictionary<T, T> pred, Dictionary<T, int> dist)
        {
            var queue = new Queue<T>();
            var visited = new HashSet<T>
            {
                src
            };

            dist[src] = 0;
            queue.Enqueue(src);

            while (queue.Count > 0)
            {
                var u = queue.Dequeue();
                var destinations = adjacencyList[u];

                foreach (var destination in destinations)
                {
                    if (!visited.Contains(destination))
                    {
                        visited.Add(destination);
                        pred[destination] = u;

                        var prevDistance = 0;
                        dist.TryGetValue(u, out prevDistance);
                        dist[destination] = prevDistance + 1;

                        queue.Enqueue(destination);

                        if (destination.Equals(goal))
                            return true;
                    }
                }
            }

            return false;
        }

        private static (T s, T d)[] GetPath(List<T> visited)
        {
            var output = new List<(T s, T d)>();

            for (int i = 0; i < visited.Count - 1; i++)
            {
                var s = visited[i];
                var d = visited[i + 1];
                output.Add((s, d));
            }

            return output.ToArray();
        }

        public (T s, T d)[] ShortestPath(T start, T goal)
        {
            var pred = new Dictionary<T, T>();
            var dist = new Dictionary<T, int>();

            if (!DistanceBFS(start, goal, pred, dist))
            {
                return Enumerable.Empty<(T s, T d)>().ToArray();
            }

            var path = new List<T>();
            T crawl = goal;
            path.Add(crawl);

            while (pred.ContainsKey(crawl))
            {
                path.Add(pred[crawl]);
                crawl = pred[crawl];
            }

            return GetPath(path);
        }
    }
}
