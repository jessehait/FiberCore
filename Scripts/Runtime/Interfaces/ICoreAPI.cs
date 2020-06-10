namespace Fiber.Core
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
        IRegistryManager    PrefData           { get; }
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
        /// Message system
        /// </summary>
        IMessageManager     Message { get; }

        /// <summary>
        /// Active core configuration file (SO)
        /// </summary>
        FiberCoreSettings   Configurations     { get; }

    }
}