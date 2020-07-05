using Fiber.Message;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fiber
{
    public class FiberCore_MessageManager: Manager, IMessageManager
    {
        private List<IFiberMessageReceiver> _list = new List<IFiberMessageReceiver>();

        public override void Initialize()
        {

        }

        public void Publish<T>(T message)
        {
            foreach (var item in _list)
            {
                if (item.Compare(typeof(T)))
                {
                    item.Execute(message);
                }
            }
        }

        public IObservableMessage<T> Receive<T>()
        {
            var bind = new FiberMessageReceiver<T>(typeof(T), this);

            return bind;
        }

        internal void Bind(IFiberMessageReceiver message, MonoBehaviour target)
        {
            if (target.TryGetComponent<FiberMessageObserver>(out var x))
            {
                x.Observe(message);
            }
            else
            {
                target.gameObject.AddComponent<FiberMessageObserver>().Observe(message);
            }
        }

        internal void Remove(IFiberMessageReceiver target)
        {
            if (_list.Contains(target))
                _list.Remove(target);
        }

        internal void Add(IFiberMessageReceiver target)
        {
            _list.Add(target);
        }
    }
}

