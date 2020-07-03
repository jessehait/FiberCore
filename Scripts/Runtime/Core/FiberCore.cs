using UnityEngine;
using Fiber.Common;
using System.Collections.Generic;

namespace Fiber
{
    [AddComponentMenu(""), DisallowMultipleComponent]
    public sealed class FiberCore : MonoBehaviour
    {
        #region Public

        public static IInstanceManager   Instances      { get; private set; }
        public static IDelayManager      Delays         { get; private set; }
        public static IUIManager         UI             { get; private set; }
        public static IFileDataManager   FileData       { get; private set; }
        public static IPrefDataManager   PrefData       { get; private set; }
        public static IMessageManager    Message        { get; private set; }
        public static IAudioManager      Audio          { get; private set; }
        public static IResourceManager   Resources      { get; private set; }
        public static IFPSManager        FPS            { get; private set; }
        public static IPoolManager       Pool           { get; private set; }
        public static ICoroutineManager  Coroutine      { get; private set; }
        public static FiberCore_Settings Configurations { get; private set; }

        #endregion

        #region Internal

        internal static bool             IsInitialized  { get; private set; }
        internal static string           AppPath        { get; private set; }
        internal static string           AppDataPath    { get; private set; }
        internal static string           AppName        { get; private set; }
        internal static string           ResourceList   { get; private set; }
        internal static Transform        Root           { get; private set; }

        #endregion

        #region Private

        private List<Manager>           managers       = new List<Manager>();

        #endregion



        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            if (!IsInitialized)
            {
                var fiberCore = new GameObject("[FiberCore]").AddComponent<FiberCore>();

                Root = fiberCore.transform;

                DontDestroyOnLoad(fiberCore);

                fiberCore.GetConfig();
                fiberCore.Load();

                IsInitialized = true;
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
            Coroutine = CreateManager<FiberCore_CoroutineManager>();
            Instances = CreateManager<FiberCore_InstanceManager>();
            Delays    = CreateManager<FiberCore_DelayManager>();
            UI        = CreateManager<FiberCore_UIManager>();
            FileData  = CreateManager<FiberCore_FileDataManager>();
            PrefData  = CreateManager<FiberCore_PrefDataManager>();
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
            InitializeManagers();
        }
    }
}