using UnityEngine;

namespace Fiber.Pools
{
    public interface IPool
    {
        IPoolElement GetElement(Transform newParent = null);
        IPoolElement[] GetElements(int count, Transform newParent = null);
        T[] GetElements<T>(int count, Transform newParent = null) where T : PoolElement;
        void PutElement(PoolElement element, int count);
        void ReturnElement(PoolElement element);
    }
}