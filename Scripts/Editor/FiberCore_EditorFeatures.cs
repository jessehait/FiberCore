using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Fiber.Editor
{
    public static class FiberCore_EditorFeatures
    {
        private static readonly string _resourcesPath;
        private static readonly string _manifestPath;
        private static readonly string _settingsPath;

        static FiberCore_EditorFeatures()
        {
            _resourcesPath = Application.dataPath + "/Resources";
            _manifestPath  = "/ResourcesInfo";
            _settingsPath  = "/FiberCoreSettings.asset";
        }

        private static void CheckResourceDirectory()
        {
            if(!Directory.Exists(_resourcesPath))
            {
                AssetDatabase.CreateFolder("Assets", "Resources");
                AssetDatabase.Refresh();
            }
        }

        private static void FillResourcesManifest()
        {
            var allResources = Directory.GetFiles(_resourcesPath, "*.*", SearchOption.AllDirectories).Where(name => !name.EndsWith(".meta")).ToArray();

            File.Create(_resourcesPath + _manifestPath).Close();

            using (StreamWriter sw = File.AppendText(_resourcesPath + _manifestPath))
            {
                for (int i = 0; i < allResources.Length; i++)
                {
                    allResources[i] = allResources[i].Replace(@"\", "/").Replace(_resourcesPath + "/", "");
                    sw.WriteLine(allResources[i]);
                }
            }
        }

        public static void CheckResourcesManifest()
        {
            CheckResourceDirectory();

            if (!File.Exists(_resourcesPath + _manifestPath))
            {
                var newAsset = new TextAsset();
                AssetDatabase.CreateAsset(newAsset, "Assets/Resources" + _manifestPath);
                SaveAndRefresh();
            }

            FillResourcesManifest();
        }

        public static void CheckFiberSettingsFile()
        {
            CheckResourceDirectory();

            if (!File.Exists(_resourcesPath + _settingsPath))
            {
                var newAsset = ScriptableObject.CreateInstance<FiberCoreSettings>();
                AssetDatabase.CreateAsset(newAsset, "Assets/Resources" + _settingsPath);
                SaveAndRefresh();
            }
        }

        private static void SaveAndRefresh()
        {
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}