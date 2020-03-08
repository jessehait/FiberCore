using RHGameCore.DataManagement;
using System;
using System.Collections.Generic;

public interface IDataManager
{
    event Action OnSaveRequested;
    event Action OnLoadRequested;

    void GetSaveData<T>(out T data) where T : BasicData;
    T GetSaveData<T>() where T : BasicData;
    void GetSaveList(out IDataInfo[] data);
    void Initialize<T>() where T : BasicData, new();
    void Load(string fileName = "");
    void LoadAsync(string fileName = "", Action onComplete = null);
    void Reset<T>() where T : BasicData, new();
    void Save(string name = "", DataSaveMethod method = DataSaveMethod.Overwrite);
    void SaveAsync(string name = "", Action onComplete = null, DataSaveMethod method = DataSaveMethod.Overwrite);
}