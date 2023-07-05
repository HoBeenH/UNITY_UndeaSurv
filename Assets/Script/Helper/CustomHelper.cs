using System.Collections;
using UnityEngine;

namespace Script.Helper
{
    public static class CustomHelper
    {
        public static void GetComp<T>(this GameObject go, ref T comp) where T : Component
        { 
            if (go == null || comp != null)
                return;
            D.Assert(go.TryGetComponent(out comp), $"{typeof(T).Name}");
        }

        public static void GetCompArrInChd<T>(this GameObject go, ref T[] compArr) where T : Component
        { 
            if (go == null || compArr.Length != 0)
                return;

            compArr = go.GetComponentsInChildren<T>();
            D.Assert(compArr?.Length != 0, $"{typeof(T).Name}");
        }

        public static bool IsValidIndex(this IList col, int idx)
        {
            if (col == null)
                return false;

            if (col.Count == 0 || idx < 0)
                return false;

            return col.Count - 1 >= idx;
        }

        public static void Parse(this string a, int fallback, out int result)
        {
            if (!int.TryParse(a, out result))
                result = fallback;
        }

        public static void Parse(this string a, float fallback, out float result)
        {
            if (!float.TryParse(a, out result))
                result = fallback;
        }
        
        public static void Parse(this string a, double fallback, out double result)
        {
            if (!double.TryParse(a, out result))
                result = fallback;
        }       
        
        public static void Parse(this string a, bool fallback, out bool result)
        {
            if (!bool.TryParse(a, out result))
                result = fallback;
        }
    }
}
