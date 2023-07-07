using RotaryHeart.Lib.SerializableDictionary;
using Script.Helper;
using Script.Manager;
using UnityEngine;

namespace Script.Base
{
    public class PoolHelper : MonoBehaviour
    {
        [System.Serializable]
        public class PoolDic : SerializableDictionaryBase<PoolManager.EPoolType, PoolMonoObject>
        {
            
        }

        [SerializeField] private PoolDic m_PoolDic = new PoolDic();

        public PoolDic Pool => m_PoolDic;
        public T GetObject<T>(PoolManager.EPoolType type) where T : PoolMonoObject
        {
            m_PoolDic.TryGetValue(type, out var _result);
            D.Assert(_result != null, $"Pool Helper ERROR {type.ToString()}");
            return _result as T;
        }

        public bool CheckType(PoolManager.EPoolType type) => m_PoolDic.ContainsKey(type);
    }
}