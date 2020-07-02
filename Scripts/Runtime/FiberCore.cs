using UnityEngine;
using Fiber.Common;
using System.Collections.Generic;

namespace Fiber
{
    [AddComponentMenu(""), DisallowMultipleComponent]
    public sealed class FiberCore : MonoBehaviour
    {
        #region API
        public static IInstanceManager   Instances        { get; private set; }
        public static IDelayManager      Delays           { get; private set; }
        public static IUIManager         UI               { get; private set; }
        public static IDataManager       FileData         { get; private set; }
        public static IRegistryManager   PrefData         { get; private set; }
        public static IMessageManager    Message          { get; private set; }
        public static IAudioManager      Audio            { get; private set; }
        public static IResourceManager   Resources        { get; private set; }
        public static IFPSManager        FPS              { get; private set; }
        public static IPoolManager       Pool             { get; private set; }
        public static ICoroutineHandler  CoroutineHandler { get; private set; }
        public static FiberCore_Settings Configurations   { get; private set; }
        #endregion

        internal static bool             IsInitialized    { get; private set; }
        internal static string           AppPath          { get; private set; }
        internal static string           AppDataPath      { get; private set; }
        internal static string           AppName          { get; private set; }
        internal static string           ResourceList     { get; private set; }

        private  List<Manager>           managers         = new List<Manager>();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            if (!IsInitialized)
            {
                var coreObject = new GameObject("[FiberCore]");
                var fiberCore = coreObject.AddComponent<FiberCore>();

                fiberCore.GetConfig();
                fiberCore.Load();

                DontDestroyOnLoad(coreObject);

                IsInitialized = true;
            }
        }

        private void InitializeComponents()
        {
            if (!gameObject.TryGetComponent(out CoroutineHandler handler))
            {
                CoroutineHandler = gameObject.AddComponent<CoroutineHandler>();
            }
        }

        private void GetConfig()
        {
            #if UNITY_EDITOR
            Editor.FiberCore_EditorFeatures.CheckFiberSettingsFile();
            #endif
            Configurations = UnityEngine.Resources.Load<FiberCore_Settings>("FiberCoreSettings");
        }

        private void CreateManagers()
        {
            Instances = CreateManager<FiberCore_InstanceManager>();
            Delays    = CreateManager<FiberCore_DelayManager>();
            UI        = CreateManager<FiberCore_UIManager>();
            FileData  = CreateManager<FiberCore_DataManager>();
            PrefData  = CreateManager<FiberCore_RegistryManager>();
            Audio     = CreateManager<FiberCore_AudioManager>();
            Resources = CreateManager<FiberCore_ResourceManager>();
            Message   = CreateManager<FiberCore_MessageManager>();
            FPS       = CreateManager<FiberCore_FPSManager>();
            Pool      = CreateManager<FiberCore_PoolManager>();
        }

        private T CreateManager<T>() where T : Manager, new()
        {
            var manager = new T();
            managers.Add(manager);
            return manager;
        }

        private void FillApplicationData()
        {
            AppPath     = Application.dataPath;
            AppDataPath = Application.persistentDataPath;
            AppName     = Application.productName;
        }

        private void RefreshResources()
        {
            #if UNITY_EDITOR
            Editor.FiberCore_EditorFeatures.CheckResourcesManifest();
            #endif
        }

        private void GetResourceList()
        {
            ResourceList = (UnityEngine.Resources.Load("ResourcesInfo", typeof(TextAsset)) as TextAsset).text;
        }

        private void InitializeManagers()
        {
            foreach (var item in managers)
            {
                item.Initialize();
            }
        }

        internal void Load()
        {
            FillApplicationData();
            RefreshResources();
            GetResourceList();
            CreateManagers();
            InitializeComponents();
            InitializeManagers();
        }
    }
}