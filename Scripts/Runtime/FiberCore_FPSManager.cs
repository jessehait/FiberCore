using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FiberCore
{
    public class FiberCore_FPSManager : Manager, IFPSManager
    {
        public override void Initialize()
        {
            Start();
        }

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

            var targetFPS = (int)FiberCore.Configurations.LimitFPS;

            if (targetFPS <= 0)
            {
                targetFPS = int.MaxValue;
            }

            SwitchVSync(FiberCore.Configurations.EnableVSync);
            Limit(targetFPS);

            FiberCore.CoroutineHandler.StartCoroutine(Calculate());
        }

        public void SwitchVSync(bool value)
        {
            QualitySettings.vSyncCount = value ? 1 : 0;
        }

        public void Limit(int targetFPS)
        {
            Application.targetFrameRate = targetFPS;
        }

        public void Unlimit()
        {
            Application.targetFrameRate = int.MaxValue;
        }

        private List<int> test = new List<int>();

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