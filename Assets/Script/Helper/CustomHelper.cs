using System;
using System.Collections;
using UnityEngine;

namespace Script.Helper
{
    public static class CustomHelper
    {
        public static void GetComp<T>(this GameObject go, ref T comp, bool showMsg = true) where T : Component
        { 
            if (go == null || comp != null)
                return;
            go.TryGetComponent(out comp);
            if (showMsg)
                D.Assert(comp, $"{go.name} {typeof(T).Name}");
        }

        public static void GetCompArrInChd<T>(this GameObject go, ref T[] compArr, bool showMsg = true) where T : Component
        { 
            if (go == null || compArr.Length != 0)
                return;

            compArr = go.GetComponentsInChildren<T>();
            if (showMsg)
                D.Assert(compArr?.Length != 0, $"{go.name} {typeof(T).Name}");
        }

        public static bool IsNullOrEmpty(this ICollection col) => col == null || col.Count <= 0;

        public static void SafetySet<T>(this T comp, GameObject go , Action<T> callback) where T : Component
        {
            if (comp == null)
                go.GetComp(ref comp);

            if (comp == null)
                return;
            
            callback?.Invoke(comp);
        }
        
        public static bool IsValidIndex(this ICollection col, int idx)
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
        
        private struct Shell<T> where T : unmanaged, Enum
        {
            public int I32Value;
            public T EnumValue;
        }
        
        public static unsafe int ToInt<TEnum>(this TEnum key) where TEnum : unmanaged, Enum
        {
            var _shell = new Shell<TEnum>
            {
                EnumValue = key
            };
            var _pi = &_shell.I32Value;
            _pi += 1;
            return *_pi;
        }
        
        public static unsafe TEnum ToEnum<TEnum>(this int iValue) where TEnum : unmanaged, Enum
        {
            var _shell = new Shell<TEnum>();
            var _pi = &_shell.I32Value;
            _pi += 1;
            *_pi = iValue;
            return _shell.EnumValue;
        }
    }
}
