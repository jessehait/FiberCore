using Fiber.Instances;
using System;

namespace Fiber
{
    public interface IInstanceManager
    {
        /// <summary>
        /// Calls on active instance changed
        /// </summary>
        event Action<Instance> OnInstanceChanged;

        /// <summary>
        /// Returns an active instance base controller
        /// </summary>
        /// <returns>Instance controller</returns>
        Instance GetActiveInstance();

        /// <summary>
        /// Returns an active instance extended controller
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Instance controller</returns>
        T GetActiveInstance<T>() where T : Instance;

        /// <summary>
        /// Load instance
        /// </summary>
        /// <param name="id">Scene ID</param>
        /// <param name="onComplete">Do on complete</param>
        /// <param name="method">Loading method</param>
        void LoadInstance(int id, Action<Instance> onComplete = null, InstanceLoadMethod method = InstanceLoadMethod.Replace);

        /// <summary>
        /// Load instance
        /// </summary>
        /// <param name="name">Scene name</param>
        /// <param name="onComplete">Do on complete</param>
        /// <param name="method">Loading method</param>
        void LoadInstance(string name, Action<Instance> onComplete = null, InstanceLoadMethod method = InstanceLoadMethod.Replace);

        /// <summary>
        /// Unload currently loaded instance
        /// </summary>
        /// <param name="onComplete">Do on complete</param>
        void UnloadActiveInstance(Action onComplete = null);
    }
}
