using System;

namespace Sea.Models
{
    public class Game
    {
        public DateTime CreationTime { get; set; } = DateTime.Now;

        public World World { get; set; }
    }
}
