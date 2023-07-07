using Script.Base;
using Script.Helper;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.Manager
{
    public class SystemManager : MonoSingleTone<SystemManager>
    {
        protected override void Awake()
        {
            D.L($"{Application.identifier}");
            base.Awake();
        }

        protected override void InitManager()
        {
            DontDestroyOnLoad(this);

            TableManager.Instance.Init();
            CoroutineManager.Instance.Init();
            PoolManager.Instance.Init();
            GameManager.Instance.Init();
            
            TableManager.Instance.OnLoadComplete();
            
            NextScene();
        }

        private void NextScene()
        {
            SceneManager.LoadScene("GameScene");
        }

        public void ReportError(EErrorType type, bool isCritical)
        {
            switch (type)
            {
                case EErrorType.LoadError:
                    D.E(type.ToString());
                    Application.Quit();
                    return;
                default:
                    break;
            }

            if (isCritical)
            {
                Application.Quit();
            }
        }

        protected override void DestroyManager()
        {
        }
    }
}
