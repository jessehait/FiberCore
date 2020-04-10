using RHGameCore.DataManagement;
using System;

namespace RHGameCore.Api
{
    public interface IRegistryManager
    {
        event Action OnLoadRequested;
        event Action OnSaveRequested;

        T GetSaveData<T>() where T : BasicData;
        bool GetSaveData<T>(out T data) where T : BasicData;
        void Initialize<T>() where T : BasicData, new();
        void Load(string name = "rhcore_registry_data");
        void LoadAsync(string name = "rhcore_registry_data", Action onComplete = null);
        void Reset<T>() where T : BasicData, new();
        void Save(string name = "rhcore_registry_data");
        void SaveAsync(string name = "rhcore_registry_data", Action onComplete = null);
    }
}