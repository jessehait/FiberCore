using System;

namespace Fiber.Message
{
    internal sealed class FiberMessageReceiver<T> : IFiberMessageReceiver, IObservableMessage<T>
    {
        private FiberCore_MessageManager _manager;
        private Func<T, bool>            _condition;
        private event Action<T>          _action;
        private bool                     _isSleep;
        private Type                     _type;

        internal FiberMessageObserver    Observer;

        internal FiberMessageReceiver(Type type, FiberCore_MessageManager manager)
        {
            _type    = type;
            _manager = manager;
        }

        public void Execute(object obj)
        {
            if (_isSleep) return;

            if(obj is T reference)
            {
                if (_condition != null && !_condition(reference)) return;

                _action?.Invoke(reference);
            }
        }

        public bool Compare(Type type)
        {
            return _type == type;
        }

        public void Sleep()
        {
            _isSleep = true;
        }

        public void WakeUp()
        {
            _isSleep = false;
        }

        public void Dispose()
        {
            Observer?.Remove(this);
            Observer = null;

            _manager?.Remove(this);
            _action   = null;
            _manager  = null;
            _type     = null;
        }

        public IDisposable Subscribe(Action<T> action)
        {
            _action = action;
            _manager.Add(this);

            return this;
        }

        public IObservableMessage<T> Where(Func<T, bool> condition)
        {
            _condition = condition;

            return this;
        }
    }
}

