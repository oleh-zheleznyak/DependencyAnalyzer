using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyAnalyzer.Core
{
    // TODO: implement IEquatable<Package> and override Equals and GetHashCode accordingly
    public struct Package
    {
        public Package(string name, string version)
        {
            Name = name;
            Version = version;
        }

        public string Name { get; }
        public string Version { get; }
    }
}
