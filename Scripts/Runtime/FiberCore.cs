﻿using UnityEngine;
using Fiber.Core;
using System.IO;
using UnityEditor;


namespace Fiber
{
    [AddComponentMenu(""),DisallowMultipleComponent]
    public sealed class FiberCore : MonoBehaviour
    {
        #region API
        public static IInstanceManager  Instances        { get; private set; }
        public static IDelayManager     Delays           { get; private set; }
        public static IUIManager        UI               { get; private set; }
        public static IDataManager      FileData         { get; private set; }
        public static IRegistryManager  PrefData         { get; private set; }
        public static IMessageManager   Message          { get; private set; }
        public static IAudioManager     Audio            { get; private set; }
        public static IResourceManager  Resources        { get; private set; }
        public static IFPSManager       FPS              { get; private set; }
        public static ICoroutineHandler CoroutineHandler { get; private set; }
        public static FiberCoreSettings Configurations   { get; private set; }
        #endregion

        internal static bool            IsInitialized    { get; private set; }
        internal static string          AppPath          { get; private set; }
        internal static string          AppDataPath      { get; private set; }
        internal static string          AppName          { get; private set; }
        internal static string          ResourceList     { get; private set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Create()
        {
            if (!IsInitialized)
            {
                var coreObject = new GameObject("[FiberCore]");
                var fiberCore = coreObject.AddComponent<FiberCore>();

                fiberCore.GetOrCreateConfig();
                fiberCore.Initialize();

                DontDestroyOnLoad(coreObject);

                IsInitialized = true;
            }
        }

        private void InitializeMono()
        {
            if (!gameObject.TryGetComponent(out CoroutineHandler handler))
            {
                CoroutineHandler = gameObject.AddComponent<CoroutineHandler>();
                ((FiberCore_FPSManager)FPS).Start();
            }
        }

        private void GetOrCreateConfig()
        {
            Configurations = UnityEngine.Resources.Load<FiberCoreSettings>("FiberCoreSettings");

            if (!Configurations)
            {
                var asset = ScriptableObject.CreateInstance<FiberCoreSettings>();

                AssetDatabase.CreateAsset(asset, "Assets/Resources/FiberCoreSettings.asset");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                Configurations = UnityEngine.Resources.Load<FiberCoreSettings>("FiberCoreSettings");
            }
        }

        private void CreateManagers()
        {
            Instances = new FiberCore_InstanceManager();
            Delays    = new FiberCore_DelayManager();
            UI        = new FiberCore_UIManager();
            FileData  = new FiberCore_DataManager();
            PrefData  = new FiberCore_RegistryManager();
            Audio     = new FiberCore_AudioManager();
            Resources = new FiberCore_ResourceManager();
            Message   = new FiberCore_MessageManager();
            FPS       = new FiberCore_FPSManager();
        }

        private void FillApplicationData()
        {
            AppPath     = Application.dataPath;
            AppDataPath = Application.persistentDataPath;
            AppName     = Application.productName;
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

            AssetDatabase.Refresh();
            #endif
        }

        private void RefreshResources()
        {
            #if UNITY_EDITOR
            if(Configurations.AutoUpdateResourceList)
                Editor.FiberCore_BuildPreprocess.UpdateResources(AppPath);
            #endif
        }

        private void GetResourceList()
        {
            ResourceList = (UnityEngine.Resources.Load("ResourcesInfo", typeof(TextAsset)) as TextAsset)?.text;
        }

        internal void Initialize()
        {
            FillApplicationData();
            ChechResourceList();
            RefreshResources();
            GetResourceList();
            CreateManagers();
            InitializeMono();
        }
    }
}