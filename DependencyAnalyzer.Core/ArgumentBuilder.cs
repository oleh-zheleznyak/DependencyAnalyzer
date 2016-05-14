using DependencyAnalyzer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyAnalyzer.Core
{
    class ArgumentBuilder
    {
        const char Separator = ',';
        public Tuple<PackageDependencyGraph, HashSet<Package>> ConvertFromString(string[] input)
        {
            // assuming we can only get Int.MaxValue of edges
            var numberOfEdges = int.Parse(input[0]);
            var graph = ParseGraph(input, numberOfEdges);

            var packages = ParsePackagesToInstall(input, numberOfEdges);

            return Tuple.Create(graph, packages);
        }

        private PackageDependencyGraph ParseGraph(string[] input, int numberOfEdges)
        {
            var graph = new PackageDependencyGraph();


            for (int i = 1; i < 1 + numberOfEdges; i++)
            {
                string[] edgeString = SplitAndVerify(input[i], 4);

                // TODO : refactor magic numbers
                var from = CreatePackage(edgeString[0], edgeString[1]);
                var to = CreatePackage(edgeString[2], edgeString[3]);

                graph.AddEdge(from, to);
            }

            return graph;
        }

        private HashSet<Package> ParsePackagesToInstall(string[] input, int numberOfEdges)
        {
            var packagesToInstall = new HashSet<Package>();

            for (int i = 1 + numberOfEdges; i < input.Length; i++)
            {
                string[] vertexString = SplitAndVerify(input[i], 2);
                var package = CreatePackage(vertexString[0], vertexString[1]);

                packagesToInstall.Add(package);
            }

            return packagesToInstall;
        }

        private static string[] SplitAndVerify(string input, int nuberOfParts)
        {
            var edgeString = input.Split(Separator);
            if (edgeString.Length != nuberOfParts) throw new FormatException($"invalid format of input data: {input}");
            return edgeString;
        }

        private Package CreatePackage(string name, string version)
        {
            return new Package(name, version);
        }
    }


}
