using System;
using NUnit.Framework;
using Sea.Factories;
using Sea.Models.Factories;

namespace Sea.Tests.Factories
{
    public class WorldFactory_Test
    {
        [Test]
        public void Create_Test()
        {
            var factory = new WorldFactory();

            Assert.Throws<Exception>(() =>
            {
                factory.Create(new WorldParameters { WorldSize = 0, IslandCount = 10 });
            });

            Assert.Throws<Exception>(() =>
            {
                factory.Create(new WorldParameters { WorldSize = -1, IslandCount = 10 });
            });

            Assert.Throws<Exception>(() =>
            {
                factory.Create(new WorldParameters { WorldSize = 100, IslandCount = 0 });
            });
        }
    }
}
