using System;
using UnityEngine;
using System.Threading.Tasks;
using Fiber.Core;
using System.IO;
using UnityEditor;
using System.Collections;

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
        public IMessageManager        Message          { get; private set; }
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

                DontDestroyOnLoad(coreObject);
            }
        }

        private void InitializeMono()
        {
            if(!gameObject.TryGetComponent(out CoroutineHandler handler))
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
            Message            = new FiberCore_MessageManager();
        }

        private void FillApplicationData()
        {
            AppPath      = Application.dataPath;
            AppDataPath  = Application.persistentDataPath;
            AppName      = Application.productName;
        }

        private void ChechResourceList()
        {
            #if UNITY_EDITOR
            var path = AppPath + "/Resources";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if(!File.Exists(path + "/ResourcesInfo.txt"))
            {
                using (FileStream fs = File.Create(path + "/ResourcesInfo.txt"))
                {
                    fs.Flush();
                    fs.Close();
                }
            }
            #endif
        }

        private void RefreshResources()
        {
            #if UNITY_EDITOR
            if (Configurations.AutoUpdateResourceList)
            {
                Editor.FiberCore_BuildPreprocess.UpdateResources(AppPath);
            }
            #endif
        }

        private void GetResourceList()
        {
            ResourceList = (UnityEngine.Resources.Load("ResourcesInfo", typeof(TextAsset)) as TextAsset).text;
        }

        public void InitializeAsync(FiberCoreConfig config, Action onInitialized)
        {
            StartCoroutine(Async());

            IEnumerator Async()
            {
                yield return null;
                if (!IsInitialized)
                {
                    Initialize(config);
                    onInitialized?.Invoke();
                }
                else
                {
                    Tools.Logger.LogWarning("CORE", "Core is already initialized.");
                }
            }
        }

        public void Initialize(FiberCoreConfig config)
        {
            if (!IsInitialized)
            {
                Configurations = config;

                FillApplicationData();
                ChechResourceList();
                RefreshResources();
                GetResourceList();
                InitializeManagers();
                InitializeMono();

                IsInitialized = true;

                AssetDatabase.Refresh();

                Tools.Logger.Log("CORE", "Initialization success.");
            }
            else
            {
                Tools.Logger.LogWarning("CORE", "Core is already initialized.");
            }
        }
    }
}