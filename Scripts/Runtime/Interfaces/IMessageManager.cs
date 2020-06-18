using System;
using UnityEngine;

namespace Fiber.Core
{
    public interface IMessageManager
    {
        void Publish<T>(T message);
        IDisposable Receive<T>(Action<T> onExecute);
        IDisposable Receive<T>(Action<T> onExecute, MonoBehaviour bindTarget);
    }
}