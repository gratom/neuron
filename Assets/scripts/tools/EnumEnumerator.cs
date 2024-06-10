using System;

namespace Tools
{
    public static class EnumEnumerator
    {
        public static T Next<T>(this T src) where T : Enum
        {
            T[] Arr = (T[])Enum.GetValues(src.GetType());
            int j = Array.IndexOf(Arr, src) + 1;
            return (Arr.Length == j) ? Arr[0] : Arr[j];
        }
    }
}