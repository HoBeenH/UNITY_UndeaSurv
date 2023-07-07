using System.Collections.Generic;
using UnityEngine;

namespace Script.Table.Base
{
    [System.Serializable]
    public abstract class TableNode : ScriptableObject
    {
        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public abstract void SetTableOnlyEditor(Dictionary<string, List<string>> data);

        public abstract void OnLoadTable();

        public abstract void ClearTable();
    }
}
