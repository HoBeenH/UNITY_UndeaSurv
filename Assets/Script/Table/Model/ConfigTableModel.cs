using Script.Table.Base;
using UnityEngine;

namespace Script.Table.Model
{
    [System.Serializable]
    public class ConfigTableModel : ModelBase
    {
        [SerializeField] public int I;
        [SerializeField] public float F;
        [SerializeField] public double D;
    }
}
