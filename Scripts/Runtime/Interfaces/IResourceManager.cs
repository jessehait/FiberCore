using Fiber.ResourceManagement;
using System;

namespace Fiber.Core
{
    public interface IResourceManager
    {
        /// <summary>
        /// Try to get loaded resource
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="path">Resource path</param>
        /// <param name="resource">Result</param>
        /// <returns>Success</returns>
        bool Get<T>(string path, out T resource) where T : UnityEngine.Object;

        /// <summary>
        /// Get loaded resource
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="path">Resource path</param>
        /// <returns>Resource</returns>
        T Get<T>(string path) where T : UnityEngine.Object;

        /// <summary>
        /// Try to load resource
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="path">Path</param>
        /// <returns>Success</returns>
        bool Load<T>(string path) where T : UnityEngine.Object;

        /// <summary>
        /// Load all resources in directory
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="path">Directory path</param>
        void LoadAll<T>(string path) where T : UnityEngine.Object;

        /// <summary>
        /// Load and get resource if exists
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="path">Path</param>
        /// <returns>Resource</returns>
        T LoadAndGet<T>(string path) where T : UnityEngine.Object;

        /// <summary>
        /// Unload loaded resource
        /// </summary>
        /// <param name="path">Path</param>
        /// <remarks>
        /// It will clear only resource field at manager, if you want totaly destroy resource,
        /// make sure that there is no active this resource references int your code.
        /// </remarks>
        void Unload(string path);

        /// <summary>
        /// Unload all loaded resources of specific direction
        /// </summary>
        /// <param name="path">Path</param>
        /// <remarks>
        /// <b>IMPORTANT</b>
        /// It will clear only resource field at manager, if you want totaly destroy resource,
        /// make sure that there is no active this resource references int your code.
        /// </remarks>
        void UnloadAll(string path);

        /// <summary>
        /// Unload all loaded resources
        /// </summary>
        /// <remarks>
        /// <b>IMPORTANT</b>
        /// It will clear only resource field at manager, if you want totaly destroy resource,
        /// make sure that there is no active this resource references int your code.
        /// </remarks>
        void UnloadAll();
    }
}