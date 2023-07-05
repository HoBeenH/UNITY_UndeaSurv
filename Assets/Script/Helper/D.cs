using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Script.Helper
{
    public class D
    {
        private const string DEBUG_SYMBOL = "UNITY_LOG";

        [System.Diagnostics.Conditional(DEBUG_SYMBOL)]
        public static void L(object msg, Object context = null) => Debug.Log(msg, context);

        [System.Diagnostics.Conditional(DEBUG_SYMBOL)]
        public static void W(object msg, Object context = null) => Debug.LogWarning(msg, context);

        [System.Diagnostics.Conditional(DEBUG_SYMBOL)]
        public static void E(object msg, Object context = null) => Debug.LogError(msg, context);

        [System.Diagnostics.Conditional(DEBUG_SYMBOL)]
        public static void Exception(Exception e, Object context = null) => Debug.LogException(e, context);

        [System.Diagnostics.Conditional(DEBUG_SYMBOL)]
        public static void Assert(bool condition, object msg = null, Object context = null) => Debug.Assert(condition, msg, context);
    }
}
