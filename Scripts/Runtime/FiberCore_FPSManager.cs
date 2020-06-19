using UnityEngine;
using System.Collections;

namespace Fiber.Core
{
    public class FiberCore_FPSManager : Manager, IFPSManager
    {
        public int Current
        {
            get
            {
                if (!FiberCore.Configurations.CalculateFPS)
                {
                    Tools.Logger.LogWarning("CORE.FPS", "You are trying to get current FPS, but FPS Calculation is disabled in FiberCore Settings");
                }
                return _current;
            }
        }

        private int _current;

        internal void Start()
        {
            if (!FiberCore.Configurations.CalculateFPS) return;

            FiberCore.CoroutineHandler.StartCoroutine(Calculate());
        }

        public void Limit(int targetFPS)
        {
            Application.targetFrameRate = targetFPS;
        }

        public void Unlimit()
        {
            Application.targetFrameRate = int.MaxValue;
        }

        private IEnumerator Calculate()
        {
            var deltaTime = 0f;

            while (true)
            {
                yield return null;

                deltaTime += Time.deltaTime;
                deltaTime /= 2;
                _current = (int)(1f / deltaTime);
            }
        }

    }
}