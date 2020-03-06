using RHGameCore.Managers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace RHGameCore.DataManagement
{
    public sealed class RHCore_DataManager : Manager, IDataManager
    {
        private BasicData _data;

        public void InitializeAs<T>() where T: BasicData, new()
        {
            _data = new T() as BasicData;
        }

        public void GetSaveData<T>(out T data) where T : BasicData
        {
            data = _data as T;
        }

        public T GetSaveData<T>() where T : BasicData
        {
            return _data as T;
        }

        public void GetSaveList(out IDataInfo[] basicDatas)
        {
            LoadFilesAsList(out basicDatas);
        }

        public void Save(string name = "", DataSaveMethod method = DataSaveMethod.Overwrite)
        {
            try
            {
                SaveToFile(name,method);
            }
            catch (Exception){}
        }

        public void Load(string fileName = "")
        {
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

        private void LoadFilesAsList(out IDataInfo[] data)
        {
            var path     = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\My Games\" + RHCore.AppName + @"\Save\";
            var fileList = System.IO.Directory.GetFiles(path, "*.save", System.IO.SearchOption.TopDirectoryOnly);
            var files    = new BasicData[fileList.Length];

            for (int i = 0; i < fileList.Length; i++)
            {
                files[i] = JsonUtility.FromJson<BasicData>(System.IO.File.ReadAllText(fileList[i]));
            }

            data = files;
        }

        private void LoadFromFile(out string data, string fileName = "")
        {
            var path     = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\My Games\" + RHCore.AppName + @"\Save\";
            string fileData = null;

            if (fileName == "")
            {
                if (System.IO.Directory.Exists(path))
                {

                    var fileList = System.IO.Directory.GetFiles(path, "*.save", System.IO.SearchOption.TopDirectoryOnly);

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
                data = System.IO.File.ReadAllText(path + fileName);
            }
        }

        private void SaveToFile(string name = "",DataSaveMethod method = DataSaveMethod.Overwrite)
        {
            var stringData = JsonUtility.ToJson(_data);
            var path       = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\My Games\" + RHCore.AppName + @"\Save\";

            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);

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

                System.IO.File.WriteAllText(path + name, stringData, System.Text.Encoding.ASCII);
            }
            else
            {
                if (_data.FileName == "")
                {
                    SaveToFile(name, DataSaveMethod.AsNew);
                    return;
                }

                _data.Modify(DateTime.Now);

                System.IO.File.WriteAllText(path + _data.FileName, stringData, System.Text.Encoding.ASCII);
            }
        }
    }
}

public enum DataSaveMethod
{
    AsNew,
    Overwrite,
}