using Script.Base;
using UnityEngine;

namespace Script.ETC
{
    public abstract class WeaponBase : PoolMonoObject
    {
        protected float m_Damage;
        protected int m_Per;

        public float Damage => m_Damage;

        public virtual void Init(float damage, int per, Vector3? dir = null)
        {
            m_Damage = damage;
            m_Per = per;
            if (dir.HasValue)
                SetDir(dir.Value);
        }

        protected virtual void SetDir(Vector3 dir)
        {
            
        }
    }
}