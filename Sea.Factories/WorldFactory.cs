using System;
using Sea.Models;
using Sea.Models.Factories;

namespace Sea.Factories
{
    public class WorldFactory: IWorldFactory
    {
        public World Create(WorldParameters parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            return new World
            {
                Width = parameters.WorldSize,
                Height = parameters.WorldSize
            };
        }
    }
}
