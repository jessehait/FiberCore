using RHGameCore.Instances;
using RHLib.ReactiveExtensions;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RHGameCore.Managers
{
    public sealed class RHCore_InstanceManager : Manager, IInstanceManager
    {
        public IReactiveCommand<Instance> OnInstanceChanged { get; private set; } = new ReactiveCommand<Instance>();
        public Instance _activeInstance;

        public Instance GetActiveInstance() => _activeInstance;


        public void LoadInstance(int id, Action<Instance> onComplete = null, InstanceLoadMethod method = InstanceLoadMethod.Replace)
        {
            if (method == InstanceLoadMethod.Replace && _activeInstance)
                SceneManager.UnloadSceneAsync(_activeInstance.gameObject.scene);

            var loading = SceneManager.LoadSceneAsync(id,LoadSceneMode.Additive);

            loading.completed += (x) =>
            {

                SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(id));

                var instance = UnityEngine.Object.FindObjectOfType<Instance>();

                if (instance)
                {
                  
                    instance.Initialize(id);
                    _activeInstance = instance;
                    OnInstanceChanged.Execute(instance);
                    onComplete?.Invoke(instance);
                }
                else
                {
                    RHLib.Tools.Logger.LogError("CORE.InstanceManager", "The instance id " + id + " does not contains controller. Plaase add \"Instance\" component to the scene of your instance.");
                }
            };
        }

        public void LoadInstance(string name, Action<Instance> onComplete = null, InstanceLoadMethod method = InstanceLoadMethod.Replace)
        {
            int? id;
            id = SceneManager.GetSceneByName(name).buildIndex;

            if(id.HasValue)
                LoadInstance(id.Value, onComplete, method);
            else
                RHLib.Tools.Logger.LogError("CORE.InstanceManager", "The instance named \"" + name + "\" not found. Make sure u entered correct name and acene added to build settings.");
        }

        public T GetActiveInstance<T>() where T : Instance
        {
            if (_activeInstance is T instance)
            {
                RHLib.Tools.Logger.Log("CORE.InstanceManager", "The instance id " + _activeInstance.ID + " of type: \"" + typeof(T).ToString() + "\"  was found.");

                return instance;
            }
            else
            {
                RHLib.Tools.Logger.LogError("CORE.InstanceManager", "The instance id " + _activeInstance.ID + " does not contains controller of type: \"" + typeof(T).ToString() + "\"");

                return null;
            }
        }
    }
}
public enum InstanceLoadMethod
{
    Additive,
    Replace,
}