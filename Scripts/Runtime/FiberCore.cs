using System;
using UnityEngine;
using System.Threading.Tasks;
using Fiber.Core;
using System.Runtime.CompilerServices;

namespace Fiber
{
    public sealed class FiberCore : MonoBehaviour, ICoreAPI, ICoreConditions
    {
        #region API
        public static ICoreAPI        API;
        public IInstanceManager       Instances        { get; private set; }
        public IDelayManager          Delays           { get; private set; }
        public IUIManager             UI               { get; private set; }
        public IDataManager           FileData         { get; private set; }
        public IRegistryManager       Registry         { get; private set; }
        public IAudioManager          Audio            { get; private set; }
        public IResourceManager       Resources        { get; private set; }
        public ICoroutineHandler      CoroutineHandler { get; private set; }
        public FiberCoreConfig        Configurations   { get; private set; }
        #endregion


        #region CONDITIONS
        public static ICoreConditions Conditions;
        public bool                   IsInitialized    { get; private set; }
        #endregion


        internal static string        AppPath          { get; private set; }
        internal static string        AppDataPath      { get; private set; }
        internal static string        AppName          { get; private set; }
        internal static string        ResourceList     { get; private set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Create()
        {
            if (API == null)
            {
                var coreObject = new GameObject("[FiberCore]");
                var fiberCore  = coreObject.AddComponent<FiberCore>();
                API        = fiberCore;
                Conditions = fiberCore;

                GameObject.DontDestroyOnLoad(coreObject);
            }
        }

        private void InitializeMono()
        {
            CoroutineHandler = gameObject.AddComponent<CoroutineHandler>();
        }

        private void InitializeManagers()
        {
            Instances          = new FiberCore_InstanceManager();
            Delays             = new FiberCore_DelayManager();
            UI                 = new FiberCore_UIManager();
            FileData           = new FiberCore_DataManager();
            Registry           = new FiberCore_RegistryManager();
            Audio              = new FiberCore_AudioManager();
            Resources          = new FiberCore_ResourceManager();
        }

        private void FillApplicationData()
        {
            AppPath      = Application.dataPath;
            AppDataPath  = Application.persistentDataPath;
            AppName      = Application.productName;
            ResourceList = (UnityEngine.Resources.Load("ResourcesInfo", typeof(TextAsset)) as TextAsset).text;
        }


        public async void InitializeAsync(FiberCoreConfig config, Action onInitialized)
        {
            FillApplicationData();

            Configurations = config;

            if (!IsInitialized)
            {
                await Task.Run(() =>
                {
                    InitializeManagers();
                    IsInitialized = true;
                    global::Fiber.Tools.Logger.Log("CORE", "Initialization success.");
                });

                onInitialized?.Invoke();
            }
            else
            {
                Tools.Logger.LogWarning("CORE", "Core is already initialized.");
            }

            InitializeMono();
        }

        public void Initialize(FiberCoreConfig config)
        {
            try
            {
                Configurations = config;

                FillApplicationData();
                InitializeManagers();
                IsInitialized = true;
                global::Fiber.Tools.Logger.Log("CORE", "Initialization success.");
            }
            catch (Exception)
            {

                Tools.Logger.LogWarning("CORE", "Core is already initialized.");
            }

            InitializeMono();
        }
    }
}