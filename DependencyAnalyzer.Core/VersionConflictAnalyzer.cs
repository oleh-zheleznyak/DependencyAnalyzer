using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyAnalyzer.Core
{
    public class VersionConflictAnalyzer
    {
        public bool HasVersionConflicts(PackageDependencyGraph graph, HashSet<Package> packages)
        {
            var firstVersionFound = new Dictionary<string, string>();
            foreach (var item in packages)
            {
                foreach (var package in graph.GetVerticesReachableFrom(item))
                {
                    if (!VersionIsValid(package, firstVersionFound)) return true;
                }
            }
            return false;
        }

        private bool VersionIsValid(Package package, Dictionary<string, string> firstVersionFound)
        {
            string version;
            if (firstVersionFound.TryGetValue(package.Name, out version))
            {
                return package.Version == version;
            }
            else
            {
                firstVersionFound[package.Name] = package.Version;
                return true;
            }
        }
    }
}
