using System;
using System.Collections.Generic;
using ConsoleApp1.Generators;
using NUnit.Framework;

namespace TestProject1.Naming
{
    public class UniqueNamesGenerationTest
    {
        [Test]
        public void RandomsNamesAreUniqueTest()
        {
            INameGenerator nameGenerator = new RandomNameGenerator(new Random());
            var usedNames = new List<String>();
            
            for (var i = 0; i < 100; i++)
            {
                var newName = nameGenerator.Generate();
                Assert.AreEqual(usedNames.Contains(newName), false);
                usedNames.Add(newName);
            }
        }
    }
}