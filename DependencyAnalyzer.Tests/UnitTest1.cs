using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DependencyAnalyzer.Core;
using System.IO;

namespace DependencyAnalyzer.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [DeploymentItem(@"TestData\Input000.txt")]
        [DeploymentItem(@"TestData\Output000.txt")]
        public void Test0()
        {
            Scenario(0);
        }

        [TestMethod]
        [DeploymentItem(@"TestData\Input001.txt")]
        [DeploymentItem(@"TestData\Output001.txt")]
        public void Test1()
        {
            Scenario(1);
        }

        [TestMethod]
        [DeploymentItem(@"TestData\Input002.txt")]
        [DeploymentItem(@"TestData\Output002.txt")]
        public void Test2()
        {
            Scenario(2);
        }

        [TestMethod]
        [DeploymentItem(@"TestData\Input003.txt")]
        [DeploymentItem(@"TestData\Output003.txt")]
        public void Test3()
        {
            Scenario(3);
        }

        [TestMethod]
        [DeploymentItem(@"TestData\Input004.txt")]
        [DeploymentItem(@"TestData\Output004.txt")]
        public void Test4()
        {
            Scenario(4);
        }

        [TestMethod]
        [DeploymentItem(@"TestData\Input005.txt")]
        [DeploymentItem(@"TestData\Output005.txt")]
        public void Test5()
        {
            Scenario(5);
        }

        [TestMethod]
        [DeploymentItem(@"TestData\Input006.txt")]
        [DeploymentItem(@"TestData\Output006.txt")]
        public void Test6()
        {
            Scenario(6);
        }

        private static void Scenario(int index)
        {
            var sut = new DependecyProgramRunner();

            var actual = sut.CheckConflicts(new string[] { Environment.CurrentDirectory + $"\\Input00{index}.txt" });

            var expected = File.ReadAllLines(Environment.CurrentDirectory + $"\\Output00{index}.txt")[0];

            Assert.AreEqual(expected, actual);
        }
    }
}
