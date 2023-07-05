using System;
using Script.Actor.Enemy;
using Script.Helper;
using Script.Manager;
using Script.Table;
using Script.Table.Model;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script.ETC
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Transform[] m_SpawnTr = null;

        private EnemyTable m_EnemyTable = null;
        private float m_Timer = 0f;
        private int m_Level = 0;
        
        private void Awake()
        {
            gameObject.GetCompArrInChd(ref m_SpawnTr);
            m_EnemyTable = TableManager.Instance.Table.GetTable<EnemyTable>();
        }

        private void Update()
        {
            m_Timer += Time.deltaTime;
            m_Level = Mathf.FloorToInt(Mathf.Max(GameManager.Instance.GameTime / 10f, 1));
            m_Level = Mathf.Min(m_Level, m_EnemyTable.GetLen());
            if (m_Timer > m_EnemyTable.GetData(m_Level).SpawnTime)
            {
                m_Timer = 0f;
                Spawn(); 
            }
        }

        private void Spawn()
        {
            var _pool = GameManager.Instance.Pool;
            if (_pool == null)
                return;

            var _enemy = _pool.Get(0);
            if (_enemy == null)
                return;

            _enemy.transform.position = m_SpawnTr[Random.Range(1, m_SpawnTr.Length)].position;
            Enemy _enemyComp = null;
            _enemy.GetComp(ref _enemyComp);
            if (_enemyComp == null)
                return;
            
            _enemyComp.Init(m_EnemyTable.GetData(m_Level));
        }
    }
}
