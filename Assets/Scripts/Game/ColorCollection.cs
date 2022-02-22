using UnityEngine;

namespace Game
{
    public static class ColorCollection
    {
        public static readonly Color Grey = FromHex("#919999");
        public static readonly Color Yellow = FromHex("#BCA95B");
        public static readonly Color Green = FromHex("#7FAF1B");

        private static Color FromHex(string hex)
        {
            ColorUtility.TryParseHtmlString(hex, out var color);
            return color;
        }
    }
}