using Script.Base;
using Script.Table;
using UnityEngine;

namespace Script.Manager
{
    public class TableManager : MonoSingleTone<TableManager>
    {
        private TableObject m_Table;
        public TableObject Table => m_Table;

        protected override void InitManager()
        {
            m_Table = Resources.Load<TableObject>("Table/TableData");
        }

        protected override void DestroyManager()
        { 
        }
    }
}
