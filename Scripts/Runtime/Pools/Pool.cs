using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FiberCore.Pools
{
    internal sealed class Pool : MonoBehaviour, IPool
    {
        private List<PoolElement> _pool     = new List<PoolElement>();
        private List<PoolElement> _released = new List<PoolElement>();
        private PoolElement       _original;
        private bool              _cleanUp;
        private float             _nextCleanTime;

        public void Initialize(PoolElement original)
        {
            _original = original;
            gameObject.SetActive(false);
        }

        public IPoolElement GetElement(Transform newParent = null)
        {
            var element = GetAndRelease();

            if (!element)
            {
                switch (FiberCore.Configurations.PoolExpandMethod)
                {
                    case PoolExpandMethod.Expand:

                        PutElement(_original, 1);
                        element = GetAndRelease();

                        break;
                    case PoolExpandMethod.Replace:

                        ReturnElement(_released.FirstOrDefault());
                        element = GetAndRelease();

                        break;
                }
            }

            if (element && newParent)
            {
                element.transform.SetParent(newParent);
                element.transform.localPosition = Vector3.zero;
                element.transform.localRotation = Quaternion.identity;
            }

            RefreshCleanUpTime(); 

            return element;
        }

        public void CleanUp(bool value)
        {
            _cleanUp = value;
            RefreshCleanUpTime();
        }

        public IPoolElement[] GetElements(int count, Transform newParent = null)
        {
            var result = new IPoolElement[count];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = GetElement(newParent);
            }

            return result;
        }

        public T[] GetElements<T>(int count, Transform newParent = null) where T : PoolElement
        {
            var result = new T[count];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = GetElement(newParent) as T;
            }

            return result;
        }

   
        public void PutElement(PoolElement element, int count)
        {
            for (int i = 0; i < count; i++)
            {
                var x = Duplicate(element);

                x.Initialize(this);
                _pool.Add(x);

                RefreshCleanUpTime();
            }
        }

        public void ReturnElement(PoolElement element)
        {
            if (_released.Contains(element))
                _released.Remove(element);

            _pool.Add(element);

            element.OnReturn();

            element.transform.SetParent(transform);
            element.transform.localPosition = Vector3.zero;
            element.transform.localRotation = Quaternion.identity;
        }

        internal void CleanCycle()
        {
            if (!_cleanUp) return;

            if (_nextCleanTime >= FiberCore.Configurations.PoolCleanUpRate)
            {
                RemoveFirst();
                RefreshCleanUpTime();
            }
            else
            {
                _nextCleanTime += Time.deltaTime;
            }
        }


        private void RefreshCleanUpTime()
        {
            _nextCleanTime = 0;
        }

        private PoolElement Duplicate(PoolElement element)
        {
            var result = Instantiate(element, transform);

            if(!result.gameObject.activeSelf)
            result.gameObject.SetActive(true);

            return result;
        }



        private void RemoveFirst()
        {
            var element = (PoolElement)GetElement();

            _released.Remove(element);

            Destroy(element.gameObject);
        }

        private PoolElement GetAndRelease()
        {
            if (_pool.Count > 0)
            {
                var element = _pool[0];

                _pool.Remove(element);
                _released.Add(element);

                element.OnRelease();

                element.transform.SetParent(null);

                return element;
            }

            return default;
        }
    }
}
