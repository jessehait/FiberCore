using Fiber.Coroutines;
using System.Collections;
using UnityEngine;

namespace Fiber
{
    public class FiberCore_CoroutineManager : Manager, ICoroutineManager
    {
        private CoroutineLifetime _lifetime;

        public override void Initialize()
        {
            _lifetime = new GameObject("[Module]: Coroutine").AddComponent<CoroutineLifetime>();
            _lifetime.transform.SetParent(FiberCore.Root);
        }

        public Coroutine Start(IEnumerator method)
        {
            return _lifetime.StartCoroutine(method);
        }

        public void Stop(Coroutine coroutine)
        {
            _lifetime.StopCoroutine(coroutine);
        }
    }
}