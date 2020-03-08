using RHGameCore.ResourceManagement;
using System;

namespace RHGameCore.Managers
{
    public interface IResourceManager
    {
        bool Get<T>(string path, out T resource) where T : UnityEngine.Object;
        T Get<T>(string path) where T : UnityEngine.Object;
        bool Load<T>(string path) where T : UnityEngine.Object;
        void LoadAll<T>(string path) where T : UnityEngine.Object;
        T LoadAndGet<T>(string path) where T : UnityEngine.Object;
        void Unload(string path);
        void UnloadAll(string path);
        void UnloadAll();
    }
}