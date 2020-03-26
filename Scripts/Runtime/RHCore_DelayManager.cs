using System;
using System.Collections;
using UnityEngine;

namespace RHGameCore.Api
{
    public sealed class RHCore_DelayManager : Manager, IDelayManager
    {

        public void WaitSeconds(float seconds, Action onComplete)
        {
            RHCore.API.MainThreadObserver.Root.StartCoroutine(Routine());

            IEnumerator Routine()
            {
                yield return new WaitForSeconds(seconds);

                onComplete?.Invoke();
            }
        }

        public void WaitUntil(Func<bool> condition, Action onComplete)
        {
            RHCore.API.MainThreadObserver.Root.StartCoroutine(Routine());

            IEnumerator Routine()
            {
                yield return new WaitUntil(condition);

                onComplete?.Invoke();
            }
        }
    }
}