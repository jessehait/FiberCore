using System.Collections;
using UnityEngine;

namespace Fiber
{
    public interface ICoroutineManager
    {
        Coroutine Start(IEnumerator method);
        void Stop(Coroutine coroutine);
    }
}