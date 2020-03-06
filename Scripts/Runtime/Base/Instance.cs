using UnityEngine;

namespace RHGameCore.Instances
{
    public class Instance: MonoBehaviour
    {
        public int ID { get; private set; }

        public void Initialize(int id)
        {
            this.ID = id;

            RHLib.Tools.Logger.Log("CORE.Instance", "Instance id " + id + " was loaded");
        }

        public T As<T>() where T: Instance
        {
            if (this is T instance)
            {
                return instance;
            }
            else
            {
                RHLib.Tools.Logger.LogError("CORE.Instance", "The instance id " + ID + " is not type of: \"" + typeof(T).ToString() + "\"");

                return null;
            }
        }

    }
}