using UnityEngine;
using System.Collections.Generic;

namespace Fiber.Message
{
    [DisallowMultipleComponent]
    public class MessageObserver : MonoBehaviour
    {
        private List<FiberMessage> _list = new List<FiberMessage>();

        internal void Observe(FiberMessage message)
        {
            if(!_list.Contains(message))
            {
                _list.Add(message);
            }
        }

        internal void Remove(FiberMessage message)
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
