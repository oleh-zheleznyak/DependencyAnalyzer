using DependencyAnalyzer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyAnalyzer.Core
{
    public class DependecyProgramRunner
    {

        public string CheckConflicts(string[] args)
        {
            // NOTE: reading everything in memory does not scale well, as number of packages grows
            var stringInput = new CommandLineParser().ParseAndRead(args);

            var input = new ArgumentBuilder().ConvertFromString(stringInput);

            var isConflictPresent = new VersionConflictAnalyzer().HasVersionConflicts(input.Item1, input.Item2);

            return FormatResult(isConflictPresent);
        }

        private static string FormatResult(bool isConflictPresent)
        {
            return isConflictPresent ? "FAIL" : "PASS";
        }
    }
}
