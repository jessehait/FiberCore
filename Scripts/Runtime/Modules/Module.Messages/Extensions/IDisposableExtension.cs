using System;
using UnityEngine;

namespace Fiber.Message
{
    public static class IDisposableExtension
    {
        public static IDisposable BindTo(this IDisposable message, MonoBehaviour target)
        {
            ((FiberCore_MessageManager)FiberCore.Message).Bind(message as IFiberMessageReceiver, target);

            return message;
        }
    }
}
