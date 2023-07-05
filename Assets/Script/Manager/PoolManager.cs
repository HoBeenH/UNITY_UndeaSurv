using System.Collections.Generic;
using Script.Base;
using Script.Helper;
using UnityEngine;

namespace Script.Manager
{
    public class PoolManager : MonoSingleTone<PoolManager>
    {
        [SerializeField] private GameObject[] m_PrefabArr = null;

        private List<GameObject>[] m_PoolList = null;

        protected override void InitManager()
        {
            m_PoolList = new List<GameObject>[m_PrefabArr.Length];

            var _len = m_PoolList.Length;
            for (var i = 0; i < _len; i++)
                m_PoolList[i] = new List<GameObject>();
        }

        public int GetAllEnemyPrefabCnt() => m_PrefabArr.Length; 

        public GameObject Get(int idx)
        {
            GameObject _result = null;

            if (!m_PoolList.IsValidIndex(idx) || !m_PrefabArr.IsValidIndex(idx))
                return null;

            var _curList = m_PoolList[idx];
            foreach (var _curGo in _curList)
            {
                if (_curGo == null || _curGo.activeSelf)
                    continue;

                _result = _curGo;
                _result.SetActive(true);
                break;
            }

            if (_result == null)
            {
                _result = Instantiate(m_PrefabArr[idx], transform);
                m_PoolList[idx].Add(_result);
            }
            
            return _result;
        }

        protected override void DestroyManager()
        {
        }
    }
}
