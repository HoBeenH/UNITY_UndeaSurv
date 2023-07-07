using System.Collections.Generic;
using System.Text;
using Script.Helper;
using Script.Table.Base;
using Script.Table.Model;
using UnityEngine;

namespace Script.Table
{
    public enum EConfig
    {
        Start,
        
        GameMaxTime = 1,
        
        EXP_START = 2,
        
        EXP_1 = 2,
        EXP_2 = 3,
        EXP_3 = 4,
        EXP_4 = 5,
        EXP_5 = 6,
        EXP_6 = 7,
        EXP_7 = 8,
        EXP_8 = 9,
        EXP_9 = 10,
        EXP_10 = 11,
        
        EXP_END = 12,
        
        END,
    }
    [System.Serializable]
    public class ConfigDic : IntDic<ConfigTableModel>
    {
        
    }

    [System.Serializable]
    public class ConfigTable : TableNode
    {
        [SerializeField] private ConfigDic m_EnemyTableDic = new ConfigDic();

        public int GetLen() => m_EnemyTableDic.Count;

        public ConfigTableModel GetData(EConfig type)
        {
            if (m_EnemyTableDic.TryGetValue(type.ToInt(), out var data))
            {
                return data;
            }

            return null;
        }
        
        public override void SetTableOnlyEditor(Dictionary<string, List<string>> data)
        {
            D.Assert(data != null);
            if (data == null)
                return;

            var _max = data["Index"].Count;
            var _idx = 0;
            while (_idx < _max)
            {
                var _model = new ConfigTableModel();
                data["Index"][_idx].Parse(-1, out _model.ID);
                data["I"][_idx].Parse(-1, out _model.I);
                data["F"][_idx].Parse(-1, out _model.F);
                data["D"][_idx].Parse(-1, out _model.D);
                
                if (m_EnemyTableDic.ContainsKey(_model.ID))
                    D.E($"Contains Key {_model.ID}");
                else
                    m_EnemyTableDic.Add(_model.ID, _model);
                
                _idx++;
            }
        }

        public override void OnLoadTable()
        {
        }

        public override void ClearTable()
        {
            m_EnemyTableDic.Clear();
        }

        public override string ToString()
        {
            var _sb = new StringBuilder();
            foreach (var _model in m_EnemyTableDic.Values)
                _sb.AppendLine($"{_model.ID} {_model.I} {_model.F} {_model.D}");

            return _sb.ToString();
        }
    }
}

