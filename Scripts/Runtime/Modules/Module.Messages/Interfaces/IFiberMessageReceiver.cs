using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fiber.Message
{
    internal interface IFiberMessageReceiver: IDisposable
    {
        void Execute(object obj);
        bool Compare(Type type);
        void WakeUp();
        void Sleep();
    }
}
