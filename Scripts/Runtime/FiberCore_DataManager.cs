using FiberCore.DataManagement;
using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace FiberCore.Api
{
    public sealed class FiberCore_DataManager : Manager, IDataManager
    {
        private BasicData    _data;
        private string       _path;

        public event Action OnSaveRequested;
        public event Action OnLoadRequested;

        private void CheckDirectory()
        {
            _path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\My Games\" + FiberCore.AppName + @"\Save\";

            if (!Directory.Exists(_path))
                Directory.CreateDirectory(_path);
        }

        public void Initialize<T>() where T: BasicData, new()
        {
            Reset<T>();
        }

        public bool GetSaveData<T>(out T data) where T : BasicData
        {
            try
            {
                data = _data as T;
                return true;
            }
            catch (Exception)
            {

                data = null;
                return false;
            }
           
        }

        public T GetSaveData<T>() where T : BasicData
        {
            return _data as T;
        }
      
        public void Reset<T>() where T : BasicData, new()
        {
            CheckDirectory();
            _data = new T() as BasicData;
        }

        public bool GetSaveList(out IDataInfo[] basicDatas)
        {
           return LoadFilesAsList(out basicDatas);
        }

        public void Save(string name = "", DataSaveMethod method = DataSaveMethod.Overwrite)
        {
            OnSaveRequested?.Invoke();

            try
            {
               SaveToFile(name, method);
            }
            catch (Exception){}
        }

        public void Load(string fileName = "")
        {
            OnLoadRequested?.Invoke();
            try
            {
                LoadFromFile(out var loadedData, fileName);
                JsonUtility.FromJsonOverwrite(loadedData, _data);
            }
            catch (Exception){}
        }

        public async void SaveAsync(string name = "", Action onComplete = null, DataSaveMethod method = DataSaveMethod.Overwrite)
        {
            await Task.Run(() =>
            {
                Save(name,method); 
            });

            onComplete?.Invoke();
        }

        public async void LoadAsync(string fileName = "",Action onComplete = null)
        {

            await Task.Run(() =>
            {
                Load();
            });

            onComplete?.Invoke();
        }

        private bool LoadFilesAsList(out IDataInfo[] data)
        {
            CheckDirectory();
            var fileList = System.IO.Directory.GetFiles(_path, "*.save", System.IO.SearchOption.TopDirectoryOnly);
            var files    = new BasicData[fileList.Length];

            for (int i = 0; i < fileList.Length; i++)
            {
                files[i] = JsonUtility.FromJson<BasicData>(System.IO.File.ReadAllText(fileList[i]));
            }

            if (files.Length > 0)
            {
                data = files;
                return true;
            }
            else
            {
                data = null;
                return false;
            }
        }

        private void LoadFromFile(out string data, string fileName = "")
        {
            CheckDirectory();
            string fileData = null;

            if (fileName == "")
            {
                if (System.IO.Directory.Exists(_path))
                {

                    var fileList = System.IO.Directory.GetFiles(_path, "*.save", System.IO.SearchOption.TopDirectoryOnly);

                    var lastFileDate = DateTime.MinValue;

                    foreach (var item in fileList)
                    {
                        var date = System.IO.File.GetLastWriteTime(item);
                        if (date > lastFileDate)
                        {
                            fileData = item;
                            lastFileDate = date;
                        }
                    }
                }
                data = System.IO.File.ReadAllText(fileData);
            }
            else
            {
                data = System.IO.File.ReadAllText(_path + fileName);
            }
        }

        private void SaveToFile(string name = "",DataSaveMethod method = DataSaveMethod.Overwrite)
        {
            var stringData = JsonUtility.ToJson(_data);
            CheckDirectory();

            if (!System.IO.Directory.Exists(_path))
                System.IO.Directory.CreateDirectory(_path);

            if (method == DataSaveMethod.AsNew)
            {
                var dt = DateTime.Now;
                var date = dt.ToShortDateString();
                var time = dt.ToString(@"hh\.mm\.ss");
                var saveFormat = date + "_" + time;

                if (name == "")
                {
                    name = "Auto_" + saveFormat;
                }
                name += ".save";

                _data.Create(name, dt);

                stringData = JsonUtility.ToJson(_data);

                System.IO.File.WriteAllText(_path + name, stringData, System.Text.Encoding.ASCII);
            }
            else
            {
                if (_data.FileName == "")
                {
                    SaveToFile(name, DataSaveMethod.AsNew);
                    return;
                }

                _data.Modify(DateTime.Now);

                System.IO.File.WriteAllText(_path + _data.FileName, stringData, System.Text.Encoding.ASCII);
            }
        }
        
        //private void SaveToRegistry(string name = "", DataSaveMethod method = DataSaveMethod.Overwrite)
        //{
        //    var stringData = JsonUtility.ToJson(_data);
        //    //CheckDirectory();

        //    //if (!System.IO.Directory.Exists(_path))
        //    //    System.IO.Directory.CreateDirectory(_path);

        //    if (method == DataSaveMethod.AsNew)
        //    {
        //        var dt = DateTime.Now;
        //        var date = dt.ToShortDateString();
        //        var time = dt.ToString(@"hh\.mm\.ss");
        //        var saveFormat = date + "_" + time;

        //        if (name == "")
        //        {
        //            name = "Auto_" + saveFormat;
        //        }
        //       // name += ".save";

        //        _data.Create(name, dt);

        //        stringData = JsonUtility.ToJson(_data);

        //        PlayerPrefs.SetString(name, stringData);

        //       // System.IO.File.WriteAllText(_path + name, stringData, System.Text.Encoding.ASCII);
        //    }
        //    else
        //    {
        //        if (_data.FileName == "")
        //        {
        //            SaveToRegistry(name, DataSaveMethod.AsNew);
        //            return;
        //        }

        //        _data.Modify(DateTime.Now);

        //        PlayerPrefs.SetString(name, stringData);
        //        //System.IO.File.WriteAllText(_path + _data.FileName, stringData, System.Text.Encoding.ASCII);
        //    }
        //}
    }
}

public enum DataSaveMethod
{
    AsNew,
    Overwrite,
}

public enum DataLocation
{
    File,
    Registry,
}
