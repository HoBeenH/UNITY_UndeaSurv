using Script.Base;
using Script.Helper;
using Script.Table;
using Script.Table.Base;
using Script.Table.Model;
using UnityEngine;

namespace Script.Manager
{
    public enum EErrorType
    {
        LoadError,
    }
    
    public class TableManager : MonoSingleTone<TableManager>
    {
        private TableObject m_Table;
        public TableObject Table => m_Table;

        protected override void InitManager()
        {
            m_Table = Resources.Load<TableObject>("Table/TableData");
            D.Assert(m_Table != null, "Can't Load Table");
            if (m_Table == null)
                SystemManager.Instance.ReportError(type: EErrorType.LoadError, isCritical: true);
        }

        public void OnLoadComplete()
        {
            m_Table.Init();
        }

        public int GetConfig(EConfig config, int fallback = -1) =>
            GetConfigModel(config)?.I ?? fallback;  
        
        public double GetConfig(EConfig config, double fallback = -1) =>
            GetConfigModel(config)?.I ?? fallback;  
        
        public float GetConfig(EConfig config, float fallback = -1) =>
            GetConfigModel(config)?.I ?? fallback;

        private ConfigTableModel GetConfigModel(EConfig config)
            => Table.GetTable<ConfigTable>()?.GetData(config);

        protected override void DestroyManager()
        {
            if (m_Table == null)
                return;
            
            return;
        }
    }
}
