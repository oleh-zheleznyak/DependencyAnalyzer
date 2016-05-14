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
            return new DepthFirstOrderIterator(this, source);
        }

        private class DepthFirstOrderIterator : IEnumerable<Package>
        {
            public DepthFirstOrderIterator(PackageDependencyGraph graph, Package source)
            {
                _graph = graph;
                _mark = new HashSet<Package>();
                _source = source;
                _preorder = new Queue<Package>();

                Dfs(source);
            }

            private readonly Queue<Package> _preorder;
            private readonly PackageDependencyGraph _graph;
            private readonly HashSet<Package> _mark;
            private readonly Package _source;

            public void Dfs(Package source)
            {
                _preorder.Enqueue(source);
                _mark.Add(source);

                foreach (var package in _graph._adjacencyList[source])
                {
                    if (!_mark.Contains(package))
                        Dfs(package);
                }
            }

            public IEnumerator<Package> GetEnumerator()
            {
                return _preorder.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}
