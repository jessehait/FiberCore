using RHGameCore.Managers;
using System;
using UnityEngine;
using RHLib.ReactiveExtensions;
using System.Threading.Tasks;
using RHGameCore.Instances;
using System.Collections;
using RHGameCore.DataManagement;

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
        public IDataManager        Data               { get; private set; }
        public IAudioManager       Audio              { get; private set; }
        public IResourceManager    Resources          { get; private set; }
        public IMainThreadObserver MainThreadObserver { get; private set; }
        public RHCoreConfig        Configurations      => _configurations;
        #endregion

        #region CONDITIONS
        public static ICoreConditions Conditions;
        public bool IsInitialized                     { get; private set; }
        #endregion


        internal static string    AppPath      { get; private set; }
        internal static string    AppName      { get; private set; }
        internal static string    ResouresList { get; private set; }
        //internal static TextAsset ResouresList { get; private set; }

        private RHCore()
        {
            API           = this;
            Conditions    = this;
        }

   
        void Awake()
        {
          

            DontDestroyOnLoad(this);

            var helper = new GameObject("[RHCore].Main");
            DontDestroyOnLoad(helper);

            MainThreadObserver = helper.AddComponent<MainThreadObserver>();
        }

        private void InitializeManagers()
        {
            Instances          = new RHCore_InstanceManager();
            Delays             = new RHCore_DelayManager();
            UI                 = new RHCore_UIManager();
            Data               = new RHCore_DataManager();
            Audio              = new RHCore_AudioManager();
            Resources          = new RHCore_ResourceManager();
        }

        public async void Initialize(Action onInitialize)
        {
            AppPath      = Application.dataPath;
            AppName      = Application.productName;
            ResouresList = (UnityEngine.Resources.Load("ResourcesInfo",typeof(TextAsset)) as TextAsset).text;

            if (!IsInitialized)
            {
                await Task.Run(() =>
                {
                    InitializeManagers();
                    IsInitialized = true;
                    RHLib.Tools.Logger.Log("CORE", "Initialization success.");
                });

                onInitialize?.Invoke();
            }
            else
            {
                RHLib.Tools.Logger.LogWarning("CORE", "Core is already initialized.");
            }
        }
    }
}