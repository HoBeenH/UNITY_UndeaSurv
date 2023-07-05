using System;
using Script.Actor.Player;
using Script.Base;
using Script.Helper;
using UnityEngine;

namespace Script.Manager
{
    public class GameManager : MonoSingleTone<GameManager>
    {
        [SerializeField] private Player m_Player = null;
        public Player Player => m_Player;

        [SerializeField] private PoolManager m_Pool = null;
        public PoolManager Pool => m_Pool;

        [SerializeField] private float m_GameTime;
        public float GameTime => m_GameTime;
        
        [SerializeField] private float m_GameMaxTime;

        protected override void InitManager()
        {
            if (m_Pool == null)
                m_Pool = PoolManager.Instance;
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
    }
}
