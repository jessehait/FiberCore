using System;

namespace Fiber.Api
{
    public interface ICoreAPI
    {
        /// <summary>
        /// Main instances (scenes) controller
        /// </summary>
        IInstanceManager    Instances          { get; }
        /// <summary>
        /// Delays awaitor
        /// </summary>
        IDelayManager       Delays             { get; }
        /// <summary>
        /// Main UI controller
        /// </summary>
        IUIManager          UI                 { get; }
        /// <summary>
        /// Data save/load controller using file system (PC/MAC)
        /// </summary>
        IDataManager        FileData           { get; }
        /// <summary>
        /// Data save/load controller using registry (Cross-platform)
        /// </summary>
        IRegistryManager    Registry           { get; }
        /// <summary>
        /// Main audio sources controller
        /// </summary>
        IAudioManager       Audio              { get; }
        /// <summary>
        /// Main resources controller
        /// </summary>
        IResourceManager    Resources          { get; }
        /// <summary>
        /// Mono-helper for running coroutines outside tha mono-classes
        /// </summary>
        ICoroutineHandler   CoroutineHandler   { get; }
        /// <summary>
        /// Active core configuration file (SO)
        /// </summary>
        FiberCoreConfig     Configurations     { get; }

        /// <summary>
        /// Entry point, should be loaded at Awake method of your main script e.g. "GameManager"
        /// </summary>
        void Initialize();

        /// <summary>
        /// Async Entry point, should be loaded at Awake method of your main script e.g. "GameManager"
        /// </summary>
        /// <param name="onInitialized"></param>
        void InitializeAsync(Action onInitialized);
    }
}