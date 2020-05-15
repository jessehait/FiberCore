using System;
using UnityEngine;
using System.Threading.Tasks;
using RHGameCore.Api;

namespace RHGameCore
{
    public sealed class RHCore : MonoBehaviour, ICoreAPI,ICoreConditions
    {
        [SerializeField]
        private RHCoreConfig _configurations;

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
        public RHCoreConfig        Configurations      => _configurations;
        #endregion

        #region CONDITIONS
        public static ICoreConditions Conditions;
        public bool IsInitialized                     { get; private set; }
        #endregion

        internal static string AppPath      { get; private set; }
        internal static string AppDataPath  { get; private set; }
        internal static string AppName      { get; private set; }
        internal static string ResourceList      { get; private set; }

        private RHCore()
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
            Instances          = new RHCore_InstanceManager();
            Delays             = new RHCore_DelayManager();
            UI                 = new RHCore_UIManager();
            FileData           = new RHCore_DataManager();
            Registry           = new RHCore_RegistryManager();
            Audio              = new RHCore_AudioManager();
            Resources          = new RHCore_ResourceManager();
        }

        private void FillApplicationData()
        {
            AppPath     = Application.dataPath;
            AppDataPath = Application.persistentDataPath;
            AppName     = Application.productName;
            ResourceList = (UnityEngine.Resources.Load("ResourcesInfo", typeof(TextAsset)) as TextAsset).text;
        }

        public async void Initialize(Action onInitialize)
        {
            FillApplicationData();

            if (!IsInitialized)
            {
                await Task.Run(() =>
                {
                    InitializeManagers();
                    IsInitialized = true;
                    RHGameCore.Tools.Logger.Log("CORE", "Initialization success.");
                });

                onInitialize?.Invoke();
            }
            else
            {
                RHGameCore.Tools.Logger.LogWarning("CORE", "Core is already initialized.");
            }
        }
    }
}