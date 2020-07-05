using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fiber.Message
{
    public interface IObservableMessage<T>
    {
        IObservableMessage<T> Where(Func<T,bool> condition);
        IDisposable Subscribe(Action<T> action);
    }
}
