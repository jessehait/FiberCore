using System.Collections;
using UnityEngine;

namespace Fiber
{
    public interface ICoroutineHandler
    {
        Coroutine StartCoroutine(IEnumerator method);
        void StopCoroutine(Coroutine coroutine);
    }
}