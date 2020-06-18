using System;
using Fiber.Core;

namespace Fiber.Message
{
    internal sealed class FiberMessage : IDisposable
    {
        private Type                     _type;
        private FiberCore_MessageManager _manager;
        private event Action<object>     _action;
        private bool                     _isSleep;
        private MessageObserver          _observer;

        internal FiberMessage Create<T>(Action<T> action, Type type, FiberCore_MessageManager manager)
        {
            _action  = new Action<object>(x => action((T)x));
            _type    = type;
            _manager = manager;

            return this;
        }

        internal void Execute<T>(T obj)
        {
            if (_isSleep) return;
            _action?.Invoke(obj);
        }

        internal bool Compare<T>()
        {
            return _type == typeof(T);
        }

        internal void Sleep()
        {
            _isSleep = true;
        }

        internal void WakeUp()
        {
            _isSleep = false;
        }

        public void Dispose()
        {
            _observer?.Remove(this);
            _manager?.Remove(this);
            _action   = null;
            _manager  = null;
            _observer = null;
            _type     = null;
        }
    }
}

