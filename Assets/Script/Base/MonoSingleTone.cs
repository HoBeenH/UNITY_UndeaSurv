using System;
using Script.Manager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.Base
{
    [DefaultExecutionOrder(-1)]
    public abstract class MonoSingleTone<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T m_Instance = null;

        public static T Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = FindObjectOfType<T>();
                    if (m_Instance == null)
                    {
                        var _go = new GameObject($"[SingleTone] {typeof(T).Name}");
                        m_Instance = _go.AddComponent<T>();
                        DontDestroyOnLoad(_go);
                        Debug.Log($"Init Manager {typeof(T).Name}");
                    }
                }

                return m_Instance;
            }
        }

        public static bool HasInstance() => m_Instance != null;

        private bool m_IsInit = false;

        protected virtual void Awake()
        {
            Init();
        }

        protected virtual void OnDestroy()
        {
            Destroy();
        }

        public void Init()
        {
            if (m_IsInit)
                return;

            m_IsInit = true;
            InitManager();
        }

        protected abstract void InitManager();

        public void Destroy()
        {
            if (!m_IsInit)
                return;
            
            DestroyManager();
        }

        protected abstract void DestroyManager();
    }
}
