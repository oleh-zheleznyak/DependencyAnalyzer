using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyAnalyzer.Core
{
    // TODO: consider splitting into graph builder and immutable graph
    public class PackageDependencyGraph
    {
        // NOTE: could also use an package-to-index mapping and use an array of linked lists of int
        private readonly Dictionary<Package, HashSet<Package>> _adjacencyList = new Dictionary<Package, HashSet<Package>>();

        public void AddEdge(Package from, Package to)
        {
            InitAdjacencyList(from);
            InitAdjacencyList(to);

            AddAdjacentEdge(to, _adjacencyList[from]);
        }

        private HashSet<Package> InitAdjacencyList(Package from)
        {
            HashSet<Package> adjacentEdges;
            if (!_adjacencyList.TryGetValue(from, out adjacentEdges))
            {
                _adjacencyList[from] = new HashSet<Package>();
            }

            return adjacentEdges;
        }

        private static void AddAdjacentEdge(Package to, HashSet<Package> adjacentEdges)
        {
            if (!adjacentEdges.Contains(to))
            {
                adjacentEdges.Add(to);
            }
        }

        public IEnumerable<Package> GetVerticesReachableFrom(Package source)
        {
            return new DepthFirstOrderIterator(this).Dfs(source);
        }

        private class DepthFirstOrderIterator
        {
            public DepthFirstOrderIterator(PackageDependencyGraph graph)
            {
                _graph = graph;
            }

            private readonly PackageDependencyGraph _graph;

            public IEnumerable<Package> Dfs(Package source)
            {
                var _mark = new HashSet<Package>();
                var stack = new Stack<Package>();
                stack.Push(source);

                while (stack.Count > 0)
                {
                    var current = stack.Pop();
                    _mark.Add(source);
                    yield return current;

                    foreach (var package in _graph._adjacencyList[current])
                    {
                        if (!_mark.Contains(package)) stack.Push(package);
                    }
                }
            }
        }
    }
}