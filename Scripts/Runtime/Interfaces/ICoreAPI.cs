﻿using System;

namespace RHGameCore.Api
{
    public interface ICoreAPI
    {
        IInstanceManager    Instances          { get; }
        IDelayManager       Delays             { get; }
        IUIManager          UI                 { get; }
        IDataManager        Data               { get; }
        IAudioManager       Audio              { get; }
        IResourceManager    Resources          { get; }
        IMainThreadObserver MainThreadObserver { get; }
        RHCoreConfig        Configurations     { get; }


        void Initialize(Action onInitialize);
    }
}