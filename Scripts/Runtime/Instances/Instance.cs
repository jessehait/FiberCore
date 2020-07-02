using UnityEngine;

namespace Fiber.Instances
{
    public abstract class Instance: MonoBehaviour
    {
        public int ID { get; private set; }

        internal void Initialize(int id)
        {
            this.ID = id;

            Tools.Logger.Log("CORE.Instance", "Instance id " + id + " was loaded");
            OnReady();
        }

        public T As<T>() where T: Instance
        {
            if (this is T instance)
            {
                return instance;
            }
            else
            {
                Tools.Logger.LogError("CORE.Instance", "The instance id " + ID + " is not type of: \"" + typeof(T).ToString() + "\"");

                return null;
            }
        }

        private void OnDestroy()
        {
            OnUnload();
        }
        protected abstract void OnReady();
        protected abstract void OnUnload();
    }
}