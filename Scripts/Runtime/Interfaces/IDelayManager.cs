using System;

namespace Fiber.Api
{
    public interface IDelayManager
    {
        /// <summary>
        /// Await seconds at DeltaTime
        /// </summary>
        /// <param name="seconds">Seconds</param>
        /// <param name="onComplete">Don on complete</param>
        void WaitSeconds(float seconds, Action onComplete);

        /// <summary>
        /// Wait until condition
        /// </summary>
        /// <param name="condition">Condition</param>
        /// <param name="onComplete">Do on complete</param>
        void WaitUntil(Func<bool> condition, Action onComplete);
    }
}