using System;
using System.Collections;
using Script.Base;
using Script.ETC;
using Script.Helper;
using Script.Manager;
using Script.Table.Model;
using UnityEngine;

namespace Script.Actor.Enemy
{
    public class Enemy : PoolMonoObject
    {
        [SerializeField] private Rigidbody2D m_TargetRig;
        [SerializeField] private RuntimeAnimatorController[] m_AminCtrl = null;
        [SerializeField] private Collider2D m_Col = null;
        private float m_Health;
        
        private Animator m_Anim = null;
        private SpriteRenderer m_Sr = null;
        private Rigidbody2D m_Rb = null;
        private bool m_IsLive = false;
        private readonly int r_HitHash = Animator.StringToHash(HIT);
        private readonly int r_DeadHash = Animator.StringToHash("Dead");
        private const string HIT = "Hit";
        private Coroutine m_Coroutine = null;
        private EnemyTableModel m_Model = null;

        public override void OnCreate()
        {
            gameObject.GetComp(ref m_Rb);
            gameObject.GetComp(ref m_Sr);
            gameObject.GetComp(ref m_Anim);
            gameObject.GetComp(ref m_Col);
        }

        private void FixedUpdate()
        {
            if (m_Model == null || !m_IsLive || m_Anim.GetCurrentAnimatorStateInfo(0).IsName(HIT))
                return;
        
            var _curPos = m_Rb.position;
            var _dir = m_TargetRig.position - _curPos;
            var _next = _dir.normalized * (m_Model.Speed * Time.fixedDeltaTime);
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
            m_Health = m_Model?.Health ?? 1f;
            m_Anim.SafetySet(this.gameObject,x => x.SetBool(r_DeadHash, false));
            m_Col.SafetySet(this.gameObject,x => x.enabled = true);
            m_Rb.SafetySet(this.gameObject,x => x.simulated = true);
            m_Sr.SafetySet(this.gameObject,x => x.sortingOrder = 2);
        }

        public void Init(EnemyTableModel data)
        {
            m_Model = data;
            m_Anim.runtimeAnimatorController = m_AminCtrl[data.ID - 1];
            m_Health = data.Health;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag(ConstHelper.BULLET_TAG) || !m_IsLive) 
                return;
            
            WeaponBase _bullet = null;
            other.gameObject.GetComp(ref _bullet);
            if (_bullet == null)
                return;

            m_Health -= _bullet.Damage;
            CoroutineManager.Instance.StartCo(CoKnockBack(), ref m_Coroutine);

            if (m_Health > 0)
            {
                m_Anim.SetTrigger(r_HitHash);
            }
            else
            {
                m_IsLive = false;
                m_Col.enabled = false;
                m_Rb.simulated = false;
                m_Anim.SetBool(r_DeadHash, true);
                m_Sr.sortingOrder = 1;
                GameManager.Instance.OnKill();
                GameManager.Instance.GetExp();
            }
        }

        private IEnumerator CoKnockBack()
        {
            yield return CoroutineManager.Instance.WaitForSec(null);

            var _playerPos = GameManager.Instance.Player.transform.position;
            var _dir = transform.position - _playerPos;
            m_Rb.AddForce(_dir.normalized * 4, ForceMode2D.Impulse);
        }

        public void Dead()
        {
            PoolManager.Instance.ReturnObj(PoolManager.EPoolType.Enemy, this);
        }
    }
}
