using FiberCore.DataManagement;
using System;

namespace FiberCore.Api
{
    public interface IRegistryManager
    {
        /// <summary>
        /// Calls before every data save
        /// </summary>
        event Action OnLoadRequested;

        /// <summary>
        /// Calls before every data load
        /// </summary>
        event Action OnSaveRequested;

        /// <summary>
        /// Register your data type
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        void Initialize<T>() where T : BasicData, new();

        /// <summary>
        /// Returns data of initialized type
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <returns>Data</returns>
        T GetSaveData<T>() where T : BasicData;

        /// <summary>
        /// Try to get saver data of your type
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="data">Data reference</param>
        /// <returns>Success</returns>
        bool GetSaveData<T>(out T data) where T : BasicData;

        /// <summary>
        /// Load last saved or specific data request
        /// </summary>
        /// <param name="name">Custome name</param>
        void Load(string name = "rhcore_registry_data");

        /// <summary>
        /// Async load last saved or specific data request
        /// </summary>
        /// <param name="name">Custome name</param>
        /// <param name="onComplete">Do on complete</param>
        void LoadAsync(string name = "fibercore_registry_data", Action onComplete = null);

        /// <summary>
        /// Clear currently loaded data object (does not clear file itself)
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        void Reset<T>() where T : BasicData, new();

        /// <summary>
        /// Save data request
        /// </summary>
        /// <param name="name">Custome name</param>
        void Save(string name = "fibercore_registry_data");

        /// <summary>
        /// Async save data request
        /// </summary>
        /// <param name="name">Custome name</param>
        /// <param name="onComplete">Do on complete</param>
        void SaveAsync(string name = "fibercore_registry_data", Action onComplete = null);
    }
}