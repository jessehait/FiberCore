using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Fiber.MessageManagement
{
    public class FiberMessageObserver : IDisposable
    {
        private Type                       _type;
        private List<FiberMessageObserver> _list;
        private Action<object>             _action;

        internal FiberMessageObserver Create<T>(Action<T> action, Type type, ref List<FiberMessageObserver> list)
        {
            _action = new Action<object>(x => action((T)x));
            _type = type;
            _list = list;

            return this;
        }

        internal void Execute<T>(T obj)
        {
            _action?.Invoke(obj);
        }

        internal bool Compare<T>()
        {
            return _type == typeof(T);
        }

        public void Dispose()
        {
            if (_list.Contains(this))
                _list.Remove(this);
        }
    }
}

