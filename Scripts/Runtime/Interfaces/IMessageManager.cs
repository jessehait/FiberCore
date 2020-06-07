using System;

namespace Fiber.Core
{
    public interface IMessageManager
    {
        void Publish<T>(T message);
        IDisposable Receive<T>(Action<T> onExecute);
    }
}