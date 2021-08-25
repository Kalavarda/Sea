using System;
using Sea.Models;
using Sea.Models.Factories;
using Sea.Models.Geometry;

namespace Sea.Factories
{
    public class WorldFactory: IWorldFactory
    {
        private static readonly Random Rand = new Random();

        public World Create(WorldParameters parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            if (parameters.WorldSize <= 1)
                throw new Exception("Слишком маленький мир");

            if (parameters.IslandCount < 1)
                throw new Exception("Нужно указать хотя бы один остров");

            var islands = CreateIslands(parameters);
            var world = new World
            {
                Size = new SizeF
                {
                    Width = parameters.WorldSize,
                    Height = parameters.WorldSize,
                },
                Islands = islands,
                Ship = new Ship()
            };
            world.Ship.Position = GetFreePosition(world);
            return world;
        }

        private static PointF GetFreePosition(World world)
        {
            // TODO: найти свободную точку

            return new PointF
            {
                X = world.Size.Width * (float)Rand.NextDouble() - world.Size.Width / 2,
                Y = world.Size.Height * (float)Rand.NextDouble() - world.Size.Height / 2
            };
        }

        private static Island[] CreateIslands(WorldParameters parameters)
        {
            var worldArea = parameters.WorldSize * parameters.WorldSize;
            var defaultIslandArea = (worldArea / parameters.IslandCount) / 10;
            var defaultIslandSize = MathF.Sqrt(defaultIslandArea);

            var islands = new Island[parameters.IslandCount];
            for (var i = 0; i < islands.Length; i++)
            {
                var angle = 2 * MathF.PI * (float)Rand.NextDouble();
                var offset = parameters.WorldSize/2 * (float)Rand.NextDouble(); // TODO: расположить по квадрату, а не по кругу
                var x = parameters.WorldSize / 2 + MathF.Cos(angle) * offset;
                var y = parameters.WorldSize / 2 + MathF.Sin(angle) * offset;

                islands[i] = new Island
                {
                    Points = new [] // TODO: сделать сложную форму
                    {
                        new PointF { X = x - defaultIslandSize / 2, Y = y - defaultIslandSize / 2},
                        new PointF { X = x - defaultIslandSize / 2, Y = y + defaultIslandSize / 2},
                        new PointF { X = x + defaultIslandSize / 2, Y = y + defaultIslandSize / 2},
                        new PointF { X = x + defaultIslandSize / 2, Y = y - defaultIslandSize / 2}
                    }
                };
            }
            return islands;
        }
    }
}
