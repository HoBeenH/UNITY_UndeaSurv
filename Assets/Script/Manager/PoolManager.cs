using System.Collections.Generic;
using Script.Base;
using Script.ETC;
using Script.Helper;
using UnityEngine;

namespace Script.Manager
{
    public class PoolManager : MonoSingleTone<PoolManager>
    {
        public enum EPoolType
        {
            Enemy = 0,
            Sword = 1,
            Bullet = 2,
            
            None = -1,
        }
        
        private readonly Dictionary<EPoolType, ObjPool> m_PoolDic = new Dictionary<EPoolType, ObjPool>();
        private PoolHelper m_PoolHelper = null;
        
        protected override void InitManager()
        {
            if (m_PoolHelper != null)
                return;
            
            DontDestroyOnLoad(gameObject);
            
            m_PoolHelper = FindObjectOfType<PoolHelper>();
            m_PoolHelper.transform.parent = transform;
            // Init Set
            if (!m_PoolDic.ContainsKey(EPoolType.Bullet))
            {
                var _pool = new ObjPool();
                _pool.InitPool<Bullet>(EPoolType.Bullet);
                m_PoolDic.Add(EPoolType.Bullet, _pool);
            }
        }

        public T GetObject<T>(EPoolType type) where T : PoolMonoObject
        {
            if (m_PoolDic.TryGetValue(type, out var _result))
            {
                var _go = _result.Get<T>();
                _go.transform.parent = null;
                _go.gameObject.SetActive(true);
                return _go;
            }
            else
            {
                var _pool = new ObjPool();
                _pool.InitPool<T>(type);
                
                m_PoolDic.Add(type, _pool);
                var _go = _pool.Get<T>();
                _go.transform.parent = null;
                _go.gameObject.SetActive(true);
                return _go;
            }
        }

        public void ReturnObj(EPoolType type, PoolMonoObject obj)
        {
            if (obj == null)
                return;

            if (obj.gameObject.activeSelf)
                obj.gameObject.SetActive(false);

            if (m_PoolDic.TryGetValue(type, out var _pool))
            {
                obj.transform.parent = transform;
                _pool.Set(obj);
            }
            else
            {
                D.E("Non Pool Obj");
                GameObject.Destroy(obj);
            }
        }

        public PoolMonoObject InstantiateObj(EPoolType type)
        {
            var _go = m_PoolHelper.GetObject<PoolMonoObject>(type);
            if (_go == null)
                return null;

            return Instantiate(_go);
        }

        protected override void DestroyManager()
        {
        }
    }
}