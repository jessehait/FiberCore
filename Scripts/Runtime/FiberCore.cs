using System;
using UnityEngine;
using System.Threading.Tasks;
using Fiber.Api;

namespace Fiber
{
    public sealed class FiberCore : MonoBehaviour, ICoreAPI, ICoreConditions
    {
        [SerializeField]
        private FiberCoreConfig _configurations;

        #region API
        public static ICoreAPI     API;               // API Instance
        public IInstanceManager    Instances          { get; private set; }
        public IDelayManager       Delays             { get; private set; }
        public IUIManager          UI                 { get; private set; }
        public IDataManager        FileData           { get; private set; }
        public IRegistryManager    Registry           { get; private set; }
        public IAudioManager       Audio              { get; private set; }
        public IResourceManager    Resources          { get; private set; }
        public ICoroutineHandler   CoroutineHandler   { get; private set; }
        public FiberCoreConfig     Configurations      => _configurations;
        #endregion

        #region CONDITIONS
        public static ICoreConditions Conditions;
        public bool IsInitialized                     { get; private set; }
        #endregion

        internal static string AppPath      { get; private set; }
        internal static string AppDataPath  { get; private set; }
        internal static string AppName      { get; private set; }
        internal static string ResourceList      { get; private set; }

        private FiberCore()
        {
            API           = this;
            Conditions    = this;
        }
   
        void Awake()
        {
            DontDestroyOnLoad(this);
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


        public async void InitializeAsync(Action onInitialized)
        {
            FillApplicationData();

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
                global::Fiber.Tools.Logger.LogWarning("CORE", "Core is already initialized.");
            }
        }

        public void Initialize()
        {
            try
            {
                FillApplicationData();
                InitializeManagers();
                IsInitialized = true;
                global::Fiber.Tools.Logger.Log("CORE", "Initialization success.");
            }
            catch (Exception)
            {

                global::Fiber.Tools.Logger.LogWarning("CORE", "Core is already initialized.");
            }
        }
    }
}