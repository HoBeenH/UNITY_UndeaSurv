using Script.Helper;
using Script.Manager;
using Script.Table.Base;
using UnityEngine;

namespace Script.Table.Model
{
    [System.Serializable]
    public class WeaponTableModel : ModelBase
    {
        [SerializeField] public float Damage;
        [SerializeField] public int Count;
        [SerializeField] public int EPoolType;
        [SerializeField] public float Timer;
        [SerializeField] public float Speed;

        public PoolManager.EPoolType GetPoolType() => EPoolType.ToEnum<PoolManager.EPoolType>();
    }
}
