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

        public static bool IsTheSameColor(Color one, Color other)
        {
            return Mathf.Approximately(one.r, other.r)
                   && Mathf.Approximately(one.g, other.g)
                   && Mathf.Approximately(one.b, other.b)
                   && Mathf.Approximately(one.a, other.a);
        }
    }
}