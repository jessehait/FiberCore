using System;

namespace RHGameCore.Api
{
    public interface IDelayManager
    {
        void WaitSeconds(float seconds, Action onComplete);
        void WaitUntil(Func<bool> condition, Action onComplete);
    }
}