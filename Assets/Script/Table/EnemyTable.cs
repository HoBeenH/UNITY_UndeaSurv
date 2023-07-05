using System.Collections.Generic;
using System.Text;
using Script.Helper;
using Script.Table.Base;
using Script.Table.Model;
using UnityEngine;

namespace Script.Table
{
    [System.Serializable]
    public class EnemyDic : TableDic<EnemyTableModel>
    {
        
    }

    [System.Serializable]
    public class EnemyTable : TableNode
    {
        [SerializeField] private EnemyDic m_EnemyTableDic = new EnemyDic();

        public int GetLen() => m_EnemyTableDic.Count;

        public EnemyTableModel GetData(int idx)
        {
            if (m_EnemyTableDic.TryGetValue(idx, out var data))
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
                var _model = new EnemyTableModel();
                data["Index"][_idx].Parse(-1, out _model.ID);
                data["SpawnTime"][_idx].Parse(-1, out _model.SpawnTime);
                data["Health"][_idx].Parse(-1, out _model.Health);
                data["Speed"][_idx].Parse(-1, out _model.Speed);
                
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

        public override string ToString()
        {
            var _sb = new StringBuilder();
            foreach (var _model in m_EnemyTableDic.Values)
                _sb.AppendLine($"{_model.ID} {_model.Health} {_model.SpawnTime} {_model.Speed}");

            return _sb.ToString();
        }
    }
}
