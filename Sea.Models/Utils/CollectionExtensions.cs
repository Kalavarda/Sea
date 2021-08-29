using System;
using System.Linq;

namespace Sea.Models.Utils
{
    public static class CollectionExtensions
    {
        public static T[] Add<T>(this T[] array, T value)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));

            var list = array.ToList();
            list.Add(value);
            return list.ToArray();
        }
    }
}
