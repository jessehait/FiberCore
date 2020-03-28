using RHGameCore.Instances;
using RHLib.ReactiveExtensions;
using System;

namespace RHGameCore.Api
{
    public interface IInstanceManager
    {
        event Action<Instance> OnInstanceChanged;
        Instance GetActiveInstance();
        T GetActiveInstance<T>() where T : Instance;
        void LoadInstance(int id, Action<Instance> onComplete = null, InstanceLoadMethod method = InstanceLoadMethod.Replace);
        void LoadInstance(string name, Action<Instance> onComplete = null, InstanceLoadMethod method = InstanceLoadMethod.Replace);
    }
}
