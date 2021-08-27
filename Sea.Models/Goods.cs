using System.Diagnostics;

namespace Sea.Models
{
    [DebuggerDisplay("id={Id} {Name}")]
    public class Goods
    {
        public string Name { get; set; }
        
        public uint Id { get; set; }
    }
}
