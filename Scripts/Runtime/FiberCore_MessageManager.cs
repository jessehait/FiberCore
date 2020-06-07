using Fiber.MessageManagement;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fiber.Core
{
    public class FiberCore_MessageManager: IMessageManager
    {
        private List<FiberMessageObserver> _list = new List<FiberMessageObserver>();

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
            var bind = new FiberMessageObserver().Create(onExecute, typeof(T), ref _list);

            _list.Add(bind);

            return bind;
        }
    }
}

