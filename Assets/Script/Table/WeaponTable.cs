using System.Collections.Generic;
using System.Text;
using Script.Helper;
using Script.Table.Base;
using Script.Table.Model;
using UnityEngine;

namespace Script.Table
{
    [System.Serializable]
    public class WeaponDic : IntDic<WeaponTableModel>
    {
        
    }

    [System.Serializable]
    public class WeaponTable : TableNode
    {
        [SerializeField] private WeaponDic m_WeaponTableDic = new WeaponDic();

        public int GetLen() => m_WeaponTableDic.Count;

        public WeaponTableModel GetData(int idx)
        {
            if (m_WeaponTableDic.TryGetValue(idx, out var data))
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
                var _model = new WeaponTableModel();
                data["Index"][_idx].Parse(-1, out _model.ID);
                data["Damage"][_idx].Parse(-1, out _model.Damage);
                data["Count"][_idx].Parse(-1, out _model.Count);
                data["EPoolType"][_idx].Parse(-1, out _model.EPoolType);
                data["Timer"][_idx].Parse(-1, out _model.Timer);
                data["Speed"][_idx].Parse(-1, out _model.Speed);
                
                if (m_WeaponTableDic.ContainsKey(_model.ID))
                    D.E($"Contains Key {_model.ID}");
                else
                    m_WeaponTableDic.Add(_model.ID, _model);
                
                _idx++;
            }
        }

        public override void OnLoadTable()
        {
        }

        public override void ClearTable()
        {
            m_WeaponTableDic.Clear();
        }

        public override string ToString()
        {
            var _sb = new StringBuilder();
            foreach (var _model in m_WeaponTableDic.Values)
                _sb.AppendLine($"{_model.ID} {_model.Damage} {_model.Count} {_model.EPoolType} {_model.Timer} {_model.Speed} {_model.GetPoolType().ToString()}");

            return _sb.ToString();
        }
    }
}
