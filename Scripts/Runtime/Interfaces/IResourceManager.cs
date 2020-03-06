using RHGameCore.ResourceManagement;
using System;

namespace RHGameCore.Managers
{
    public interface IResourceManager
    {
        bool Get<T>(string path, out T resource) where T : UnityEngine.Object;
        T Get<T>(string path) where T : UnityEngine.Object;
        void Load<T>(string path) where T : UnityEngine.Object;
        void LoadAll<T>(string path) where T : UnityEngine.Object;
    }
}