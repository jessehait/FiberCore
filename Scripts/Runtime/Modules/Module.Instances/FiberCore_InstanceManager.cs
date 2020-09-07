using Fiber.Instances;
using System;
using UnityEngine.SceneManagement;

namespace Fiber
{
    public sealed class FiberCore_InstanceManager : Manager, IInstanceManager
    {
        public event Action<Instance> OnInstanceChanged;
        public Instance _activeInstance;

        public Instance GetActiveInstance() => _activeInstance;

        public override void Initialize()
        {

        }

        public void FindPreloadedInstance()
        {
            var instance = UnityEngine.Object.FindObjectOfType<Instance>();

            if (instance)
            {

                var id = instance.gameObject.scene.buildIndex;
                instance.Initialize(id);
                _activeInstance = instance;
                OnInstanceChanged?.Invoke(instance);
            }
        }

        public void LoadInstance(int id, Action<Instance> onComplete = null, InstanceLoadMethod method = InstanceLoadMethod.Replace)
        {
            if (method == InstanceLoadMethod.Replace && _activeInstance)
            {
                SceneManager.UnloadSceneAsync(_activeInstance.gameObject.scene);
            }

            var loading = SceneManager.LoadSceneAsync(id,LoadSceneMode.Additive);

            loading.completed += (x) =>
            {

                SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(id));

                var instance = UnityEngine.Object.FindObjectOfType<Instance>();

                if (instance)
                {
                  
                    instance.Initialize(id);
                    _activeInstance = instance;
                    OnInstanceChanged?.Invoke(instance);
                    onComplete?.Invoke(instance);
                }
                else
                {
                    Tools.Logger.LogError("CORE.InstanceManager", "The instance id " + id + " does not contains controller. Plaase add \"Instance\" component to the scene of your instance.");
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
                Tools.Logger.LogError("CORE.InstanceManager", "The instance named \"" + name + "\" not found. Make sure u entered correct name and acene added to build settings.");
        }

        public void UnloadActiveInstance(Action onComplete = null)
        {
            if (!_activeInstance) return;

            var unloading = SceneManager.UnloadSceneAsync(_activeInstance.gameObject.scene);

            unloading.completed += (x) =>
            {
                _activeInstance = null;
                OnInstanceChanged?.Invoke(null);
                onComplete?.Invoke();
            };
        }
        
        public T GetActiveInstance<T>() where T : Instance
        {
            if (_activeInstance is T instance)
            {
                Tools.Logger.Log("CORE.InstanceManager", "The instance id " + _activeInstance.ID + " of type: \"" + typeof(T).ToString() + "\"  was found.");

                return instance;
            }
            else
            {
                Tools.Logger.LogError("CORE.InstanceManager", "The instance id " + _activeInstance.ID + " does not contains controller of type: \"" + typeof(T).ToString() + "\"");

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