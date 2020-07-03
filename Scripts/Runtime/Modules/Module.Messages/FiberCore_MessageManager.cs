using Fiber.Message;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fiber
{
    public class FiberCore_MessageManager: Manager, IMessageManager
    {
        private List<FiberMessage> _list = new List<FiberMessage>();

        public override void Initialize()
        {

        }

        public void Publish<T>(T message)
        {
            foreach (var item in _list)
            {
                if (item.Compare<T>())
                {
                    item.Execute(message);
                }
            }
        }

        public IDisposable Receive<T>(Action<T> onExecute)
        {
            var bind = new FiberMessage().Create(onExecute, typeof(T), this);

            _list.Add(bind);

            return bind;
        }

        public IDisposable Receive<T>(Action<T> onExecute, MonoBehaviour bindTarget)
        {
            var bind = (FiberMessage)Receive(onExecute);

            if (bindTarget.TryGetComponent<FiberMessageObserver>(out var x))
            {
                x.Observe(bind);
            }
            else
            {
                bindTarget.gameObject.AddComponent<FiberMessageObserver>().Observe(bind);
            }

            return bind;
        }

        internal void Remove(FiberMessage target)
        {
            if (_list.Contains(target))
                _list.Remove(target);
        }
    }
}

