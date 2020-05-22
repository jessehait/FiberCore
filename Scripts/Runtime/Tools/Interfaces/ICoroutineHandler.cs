using System.Collections;
using UnityEngine;

namespace FiberCore
{
    public interface ICoroutineHandler
    {
        Coroutine StartCoroutine(IEnumerator method);
        void StopCoroutine(Coroutine coroutine);
    }
}