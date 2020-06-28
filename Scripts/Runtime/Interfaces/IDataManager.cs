using FiberCore.Data;
using System;

namespace FiberCore
{
    public interface IDataManager
    {
        /// <summary>
        /// Calls before every data save
        /// </summary>
        event Action OnSaveRequested;

        /// <summary>
        /// Calls before every data load
        /// </summary>
        event Action OnLoadRequested;

        /// <summary>
        /// Register your data type
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        void RegisterType<T>() where T : BasicData, new();

        /// <summary>
        /// Try to get saver data of your type
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="data">Data reference</param>
        /// <returns>Success</returns>
        bool GetData<T>(out T data) where T : BasicData;

        /// <summary>
        /// Returns data of initialized type
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <returns>Data</returns>
        T GetData<T>() where T : BasicData;

        /// <summary>
        /// Get list of saved data files
        /// </summary>
        /// <param name="data">Data reference</param>
        bool GetSaveList(out IDataInfo[] data);

        /// <summary>
        /// Load last saved or specific data request
        /// </summary>
        /// <param name="fileName">Custome file name</param>
        void Load(string fileName = "");

        /// <summary>
        /// Async load last saved or specific data request
        /// </summary>
        /// <param name="fileName">Custome file name</param>
        /// <param name="onComplete">Do on complete</param>
        void LoadAsync(string fileName = "", Action onComplete = null);

        /// <summary>
        /// Clear currently loaded data object (does not clear file itself)
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        void Reset<T>() where T : BasicData, new();

        /// <summary>
        /// Save data request
        /// </summary>
        /// <param name="name">Custome file name</param>
        /// <param name="method">Data save method</param>
        void Save(string name = "", DataSaveMethod method = DataSaveMethod.Overwrite);

        /// <summary>
        /// Async save data request
        /// </summary>
        /// <param name="name">Custome file name</param>
        /// <param name="onComplete">Do on complete</param>
        /// <param name="method">Data save method</param>
        void SaveAsync(string name = "", Action onComplete = null, DataSaveMethod method = DataSaveMethod.Overwrite);
    }
}