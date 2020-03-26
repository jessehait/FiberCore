using RHGameCore.Instances;
using RHLib.ReactiveExtensions;
using System;

namespace RHGameCore.Api
{
    public interface IInstanceManager
    {
        IReactiveCommand<Instance> OnInstanceChanged { get; }
        Instance GetActiveInstance();
        T GetActiveInstance<T>() where T : Instance;
        void LoadInstance(int id, Action<Instance> onComplete = null, InstanceLoadMethod method = InstanceLoadMethod.Replace);
        void LoadInstance(string name, Action<Instance> onComplete = null, InstanceLoadMethod method = InstanceLoadMethod.Replace);
    }
}
