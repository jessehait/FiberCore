using UnityEngine;

namespace Fiber.Pools
{
    public abstract class PoolElement : MonoBehaviour, IPoolElement
    {
        private IPool _pool;

        internal void Initialize(IPool pool)
        {
            _pool = pool;
        }

        public void Return()
        {
            _pool.ReturnElement(this);
        }

        public abstract void OnRelease();
        public abstract void OnReturn();
    }
}
