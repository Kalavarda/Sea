using System;
using System.Collections.Generic;
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
            var defaultIslandArea = (worldArea / parameters.IslandCount) / 100;
            var defaultIslandSize = MathF.Sqrt(defaultIslandArea);

            var islands = new Island[parameters.IslandCount];
            for (var i = 0; i < islands.Length; i++)
            {
                var angle = 2 * MathF.PI * (float)Rand.NextDouble();
                var offset = parameters.WorldSize/2 * (float)Rand.NextDouble();
                var x = MathF.Cos(angle) * offset;
                var y = MathF.Sin(angle) * offset;
                islands[i] = CreateIsland(x, y, defaultIslandSize * (0.5f + 1.5f * (float)Rand.NextDouble()));
            }
            return islands;
        }

        private static Island CreateIsland(float x, float y, float defaultSize)
        {
            var points = new List<PointF>();
            for (var i = 0; i < 8; i++)
            {
                var a = i * MathF.PI / 4;
                var r = defaultSize * (0.25f + 0.5f * (float)Rand.NextDouble());
                points.Add(new PointF { X = x + r * MathF.Cos(a), Y = y + r * MathF.Sin(a) });
            }

            return new Island
            {
                Points = points.ToArray()
            };
        }
    }
}
