using Fiber.Resources;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Logger = Fiber.Tools.Logger;

namespace Fiber.Core
{
    public enum ResourceFindMethod 
    { 
        File,
        Directory,
    }

    public sealed class FiberCore_ResourceManager : Manager, IResourceManager
    {
        private List<Resource> _resources = new List<Resource>();

        public FiberCore_ResourceManager()
        {
            GenerateResourcesList();
        }

        private void GenerateResourcesList()
        {
            if (FiberCore.ResourceList == null)
            {
                Logger.LogError("CORE.RESOURCES", "There is no ResourcesInfo found, please, use Fiber > FiberCore > Generate Resource List");
                return;
            }
            
            var list = FiberCore.ResourceList.Split('\n').Where(x => x != "").ToArray();

            foreach (var item in list)
            {
                _resources.Add(new Resource(item.Trim()));
            }
        }

        public void Unload(string path)
        {
            try
            {
                var resource = _resources.Where(x => x.GetPath() == path).SingleOrDefault();

                if (resource != null)
                {
                    resource.UnLoad();
                }
            }
            catch (System.Exception)
            {
                Logger.LogWarning("CORE.RESOURCES", "Resource you try to unload is not loaded");
            }
        }

        public void UnloadAll(string path)
        {
            foreach (var item in _resources)
            {
                if (CheckResourcePath(path, item,ResourceFindMethod.Directory) && item.IsLoaded())
                {
                    item.UnLoad();
                }
            }
        }

        public void UnloadAll()
        {
            foreach (var item in _resources)
            {
                Unload(item.GetPath());
            }
        }

        public bool Get<T>(string path, out T resource) where T : Object
        {
            foreach (var item in _resources)
            {
                if (CheckResourcePath(path, item) && item.IsLoaded())
                {
                    resource = item._object as T;
                    return true;
                }
            }
            Logger.LogError("CORE.RESOURCES", "Resource \""+ path + "\" not loaded or path is invalid.");

            resource = null;
            return false;
        }

        public T Get<T>(string path) where T : Object
        {
            Get(path, out T result);
            return result;
        }

        public bool Load<T>(string path) where T : Object
        {
            bool error = true;
            foreach (var item in _resources)
            {
                if (CheckResourcePath(path, item) && !item.IsLoaded())
                {
                    item.Load<T>();
                    error = false;
                }
            }
            if (error)
            {
                
                Logger.LogError("CORE.RESOURCES", "Resource \"" + path + "\" already loaded or path is invalid.");
                return false;
            }
            else
            {
                return true;
            }

        }

        public T LoadAndGet<T>(string path) where T : Object
        {
            if (Load<T>(path)) return Get<T>(path);
            return null;
        }

        public void LoadAll<T>(string path) where T : Object
        {
            foreach (var item in _resources)
            {
                if (CheckResourcePath(path, item, ResourceFindMethod.Directory))
                {
                    if (!item.IsLoaded())
                    {
                        item.Load<T>();
                    }
                }
            }
        }

        private bool CheckResourcePath(string path, Resource targerResource, ResourceFindMethod method = ResourceFindMethod.File)
        {
            path.Trim();

            List<string> inputPath      = new List<string>();
            List<string> inputDirectory = new List<string>();

            if (path.ToList().Contains('/'))
            {
                inputPath      = path.Split('/').ToList();
                inputDirectory = path.Split('/').ToList();

                if(inputDirectory.LastOrDefault() == "")
                inputDirectory.Remove(inputDirectory.LastOrDefault());
            }
            else
            {
                inputPath.Add(path);
                inputDirectory.Add(path);
            }

            if (method == ResourceFindMethod.File)
            {
                if (inputPath.Count > 0)
                {
                    return ListContentEquals(inputPath, targerResource._path);
                }
            }
            if (method == ResourceFindMethod.Directory)
            {
                if(path == "")
                {
                    return true;
                }

                if (inputDirectory.Count > 0)
                {
                    int countOfSame = 0;

                    for (int i = 0; i < inputDirectory.Count; i++)
                    {
                        if(targerResource._directory.Count >= inputDirectory.Count)
                        if (inputDirectory[i] == targerResource._directory[i]) countOfSame++;
                    }
                    if (countOfSame == inputDirectory.Count)
                        return true;
                }
            }
            return false;
        }

        private bool ListContentEquals(List<string> list1, List<string> list2)
        {
            var cnt = new Dictionary<string, int>();
            foreach (var item in list1)
            {
                if (cnt.ContainsKey(item))
                {
                    cnt[item]++;
                }
                else
                {
                    cnt.Add(item, 1);
                }
            }
            foreach (var item in list2)
            {
                if (cnt.ContainsKey(item))
                {
                    cnt[item]--;
                }
                else
                {
                    return false;
                }
            }
            return cnt.Values.All(c => c == 0);
        }
    }
}
