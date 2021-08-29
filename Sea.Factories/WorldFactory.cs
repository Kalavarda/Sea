using System;
using System.Collections.Generic;
using System.Linq;
using Sea.Models;
using Sea.Models.Geometry;
using Sea.Models.Interfaces;
using PointF = Sea.Models.Geometry.PointF;
using SizeF = Sea.Models.Geometry.SizeF;

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
                Ship = CreateShip()
            };
            world.Ship.Position = GetFreePosition(world);
            return world;
        }

        private static Ship CreateShip()
        {
            var ship = new Ship
            {
                Fuel = new RangeF { Max = 10 },
                GoodsMass = new RangeF { Max = 100 },
                Engine = new Engine
                {
                    FuelConsumption = 0.05f,
                    Rotation = new RangeF(-MathF.PI / 12, MathF.PI / 12),
                    Acceleration = new RangeF(-0.25f, 1)
                }
            };
            ship.Fuel.Value = ship.Fuel.Max / 2;
            return ship;
        }

        private static PointF GetFreePosition(World world)
        {
            // TODO: найти свободную точку

            var centerPoint = new PointF();
            var nearestPort = world.Islands.SelectMany(i => i.Ports)
                .OrderBy(p => p.Position.DistanceTo(centerPoint))
                .First();

            return new PointF
            {
                X = nearestPort.Position.X,
                Y = nearestPort.Position.Y
            };
        }

        private static Island[] CreateIslands(WorldParameters parameters)
        {
            var worldArea = parameters.WorldSize * parameters.WorldSize;
            var defaultIslandArea = (worldArea / parameters.IslandCount) / 1000;
            var defaultIslandSize = MathF.Sqrt(defaultIslandArea);

            var islands = new Island[parameters.IslandCount];
            for (var i = 0; i < islands.Length; i++)
            {
                var angle = 2 * MathF.PI * (float)Rand.NextDouble();
                var offset = parameters.WorldSize/2 * (float)Rand.NextDouble();
                var x = MathF.Cos(angle) * offset;
                var y = MathF.Sin(angle) * offset;
                islands[i] = CreateIsland(x, y, defaultIslandSize * (0.25f + 3.75f * (float)Rand.NextDouble()));
            }

            uint id = 1;
            foreach (var port in islands.SelectMany(i => i.Ports))
            {
                port.Id = id;
                id++;
            }

            return islands;
        }

        private static Island CreateIsland(float x, float y, float defaultSize)
        {
            var points = new List<PointF>();
            var ports = new List<Port>();
            for (var i = 0; i < 8; i++)
            {
                var a = i * MathF.PI / 4;
                var r = defaultSize * (0.25f + 0.5f * (float)Rand.NextDouble());
                var vertex = new PointF { X = x + r * MathF.Cos(a), Y = y + r * MathF.Sin(a) };
                points.Add(vertex);
                if (Rand.Next(2) == 0)
                    ports.Add(new Port { Position = vertex });
            }

            return new Island
            {
                Points = points.ToArray(),
                Ports = ports.ToArray()
            };
        }
    }
}
