using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyAnalyzer.Core
{
    class CommandLineParser
    {
        public string[] ParseAndRead(string[] args)
        {
            if (args == null) throw new ArgumentNullException(nameof(args));
            if (args.Length != 1) throw new ArgumentException("expecting exactly one argument");

            var file = new FileInfo(args[0]);

            if (!file.Exists) throw new FileNotFoundException(file.FullName);

            return File.ReadAllLines(file.FullName);
        }
    }
}
