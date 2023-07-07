using System.Collections;
using Script.Base;
using UnityEngine;

namespace Script.Manager
{
    public class CoroutineManager : MonoSingleTone<CoroutineManager>
    {
        public IEnumerator WaitForSec(float? maxTime)
        {
            if (!maxTime.HasValue)
                yield return null;
            else
            {
                for (float i = 0f; i < maxTime.Value; i += Time.deltaTime)
                    yield return null;
            }
        }

        public void StartCo(IEnumerator enumerator, ref Coroutine co)
        {
            if (co != null)
                StopCo(ref co);

            co = StartCoroutine(enumerator);
        }

        public void StopCo(ref Coroutine co)
        {
            if (co == null)
                return;
            
            StopCoroutine(co);
            co = null;
        }

        protected override void InitManager()
        { }

        protected override void DestroyManager()
        { }
    }
}
