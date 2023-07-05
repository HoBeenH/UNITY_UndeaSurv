using Script.Table.Base;
using UnityEngine;

namespace Script.Table.Model
{
    [System.Serializable]
    public class EnemyTableModel : ModelBase
    {
        [SerializeField] public float SpawnTime;
        [SerializeField] public int Health;
        [SerializeField] public float Speed;
    }
}