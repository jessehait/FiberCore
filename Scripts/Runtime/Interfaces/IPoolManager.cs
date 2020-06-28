using FiberCore.Pools;
using UnityEngine;

namespace FiberCore
{
    public interface IPoolManager
    {
        T GetElement<T>(Transform newParent = null) where T : PoolElement;
        T[] GetElements<T>(int count = 1, Transform newParent = null) where T : PoolElement;
        IPool GetPool<T>() where T : PoolElement;
        IPool PutElement<T>(T prefab, int count = 1) where T : PoolElement;
        IPool PutElement<T>(T prefab, int count = 1, bool startCleaningCycle = false) where T : PoolElement;
    }
}