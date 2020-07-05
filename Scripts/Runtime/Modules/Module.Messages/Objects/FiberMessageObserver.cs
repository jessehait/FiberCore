using UnityEngine;
using System.Collections.Generic;
using System;

namespace Fiber.Message
{
    [DisallowMultipleComponent]
    public class FiberMessageObserver : MonoBehaviour
    {
        private List<IFiberMessageReceiver> _list = new List<IFiberMessageReceiver>();

        internal void Observe(IFiberMessageReceiver message)
        {
            if(!_list.Contains(message))
            {
                _list.Add(message);
            }
        }

        internal void Remove(IFiberMessageReceiver message)
        {
            if (_list.Contains(message))
            {
                _list.Remove(message);
            }
        }

        private void OnEnable()
        {
            for (int i = 0; i < _list.Count; i++)
            {
                _list[i].WakeUp();
            }
        }

        private void OnDisable()
        {
            for (int i = 0; i < _list.Count; i++)
            {
                _list[i].Sleep();
            }
        }

        private void OnDestroy()
        {
            for (int i = 0; i < _list.Count; i++)
            {
                _list[i].Dispose();
            }
        }
    }
}
