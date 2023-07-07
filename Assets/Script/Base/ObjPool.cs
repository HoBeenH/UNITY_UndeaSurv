using System.Collections.Generic;
using Script.Helper;
using Script.Manager;
using UnityEngine;

namespace Script.Base
{
    public class ObjPool
    {
        private Queue<PoolMonoObject> m_Pool;
        private PoolManager.EPoolType m_PoolType;

        public void InitPool<T>(PoolManager.EPoolType type) where T : PoolMonoObject
        {
            m_PoolType = type;
            m_Pool = new Queue<PoolMonoObject>(8);
        }

        public T Get<T>() where T : PoolMonoObject
        {
            if (m_Pool.IsNullOrEmpty())
            {
                m_Pool.Enqueue(CreatePoolObj<T>());
            }

            return m_Pool.Dequeue() as T;
        }

        private T CreatePoolObj<T>() where T : PoolMonoObject
        {
            var _go = PoolManager.Instance.InstantiateObj(m_PoolType);
            D.Assert(_go != null, $"{typeof(T)} {m_PoolType.ToString()}");
            if (_go == null)
                return null;

            _go.gameObject.SetActive(false);
            var _comp = _go.GetComponent<T>();
            if (_comp == null)
                return null;
            
            _comp.OnCreate();

            return _comp;
        }

        public void Set(PoolMonoObject obj)
        {
            if (obj == null)
                return;

            if (obj.isActiveAndEnabled)
                obj.gameObject.SetActive(false);

            m_Pool.Enqueue(obj);
        }
    }
}