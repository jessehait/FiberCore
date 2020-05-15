using System.Collections;
using UnityEngine;

namespace RHGameCore
{
    public interface ICoroutineHandler
    {
        Coroutine StartCoroutine(IEnumerator method);
        void StopCoroutine(Coroutine coroutine);
    }
}