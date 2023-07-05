using System;
using Script.Helper;
using Script.Manager;
using Script.Table;
using Script.Table.Model;
using UnityEngine;

namespace Script.Actor.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float m_Speed;
        [SerializeField] private float m_Health;
        [SerializeField] private float m_MaxHealth;
        [SerializeField] private Rigidbody2D m_TargetRig;
        [SerializeField] private RuntimeAnimatorController[] m_AminCtrl = null;

        private Animator m_Anim = null;
        private SpriteRenderer m_Sr = null;
        private Rigidbody2D m_Rb = null;
        private bool m_IsLive = false;

        private void Awake()
        {
            gameObject.GetComp(ref m_Rb);
            gameObject.GetComp(ref m_Sr);
            gameObject.GetComp(ref m_Anim);
        }

        private void FixedUpdate()
        {
            if (!m_IsLive)
                return;
        
            var _curPos = m_Rb.position;
            var _dir = m_TargetRig.position - _curPos;
            var _next = _dir.normalized * (m_Speed * Time.fixedDeltaTime);
            m_Rb.MovePosition(_curPos + _next);
            m_Rb.velocity = Vector2.zero;
        }

        private void LateUpdate()
        {
            m_Sr.flipX = m_TargetRig.position.x < m_Rb.position.x;
        }

        private void OnEnable()
        {
            GameManager.Instance.Player.gameObject.GetComp(ref m_TargetRig);
            m_IsLive = true;
            m_Health = m_MaxHealth;
        }

        public void Init(EnemyTableModel data)
        {
            m_Anim.runtimeAnimatorController = m_AminCtrl[data.ID - 1];
            m_Speed = data.Speed;
            m_MaxHealth = data.Health;
            m_Health = m_MaxHealth;
        }
    }
}
