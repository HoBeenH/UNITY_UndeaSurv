using Script.Helper;
using Script.Manager;
using UnityEngine;

namespace Script.ETC
{
    public class Bullet : WeaponBase
    {
        private Rigidbody2D m_Rb;
        
        public override void OnCreate() => gameObject.GetComp(ref m_Rb, false);

        protected override void SetDir(Vector3 dir) => m_Rb.velocity = dir * 15f;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag(ConstHelper.ENEMY_TAG))
                return;

            m_Per--;
            if (m_Per == -1)
            {
                m_Rb.velocity = Vector2.zero;
                PoolManager.Instance.ReturnObj(PoolManager.EPoolType.Bullet ,this);
            }
        }
    }
}
