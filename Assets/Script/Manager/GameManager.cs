using System.Collections.Generic;
using Script.Actor.Player;
using Script.Base;
using Script.Helper;
using Script.Table;
using UnityEngine;

namespace Script.Manager
{
    public class GameManager : MonoSingleTone<GameManager>
    {
        [SerializeField] private Player m_Player = null;

        public Player Player
        {
            get
            {
                if (m_Player == null)
                    m_Player = FindObjectOfType<Player>();

                return m_Player;
            }
        }

        [SerializeField] private float m_GameTime;
        public float GameTime => m_GameTime;
        
        [SerializeField] private int m_Level;
        [SerializeField] private int m_Kill;
        [SerializeField] private int m_Exp;
        [SerializeField] private int[] m_NextExp;
        
        private float m_GameMaxTime;

        protected override void InitManager()
        {
            m_GameMaxTime = TableManager.Instance.GetConfig(EConfig.GameMaxTime, 30f);
            var _expList = new List<int>();
            for (EConfig i = EConfig.EXP_START; i < EConfig.EXP_END; i++)
            {
                _expList.Add(TableManager.Instance.GetConfig(i, 20));
            }

            m_NextExp = _expList.ToArray();
        }

        private void Update()
        {
            m_GameTime += Time.deltaTime;
            if (m_GameTime > m_GameMaxTime)
            {
                m_GameTime = m_GameMaxTime;
            }
        }

        protected override void DestroyManager()
        {
        }

        public void OnKill() => m_Kill++;
        public void GetExp()
        {
            m_Exp++;
            if (m_Exp == m_NextExp[m_Level])
            {
                m_Level++;
                m_Exp = 0;
            }
        }
    }
}
