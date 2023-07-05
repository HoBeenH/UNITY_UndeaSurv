using System;
using System.Collections.Generic;
using Script.Table.Base;
using UnityEngine;

namespace Script.Table
{
    [System.Serializable]
    public class TableObject : ScriptableObject
    {
        [SerializeField] private List<ScriptableObject> m_TableList = new List<ScriptableObject>();
        public List<ScriptableObject> TableList => m_TableList;
        public T GetTable<T>() where T : class => m_TableList.Find(x => x is T) as T;

        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public void AddData(ScriptableObject so)
        {
            if (!m_TableList.Contains(so))
                m_TableList.Add(so);
        }

        public void Init()
        {
            foreach (var so in m_TableList)
            {
                var _node = so as TableNode;
                if (_node != null)
                    _node.OnLoadTable();
            }
        }
    }
}
