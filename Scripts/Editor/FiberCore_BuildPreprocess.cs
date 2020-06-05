using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Fiber.Editor
{
    public class FiberCore_BuildPreprocess : IPreprocessBuildWithReport
    {
        public int callbackOrder { get { return 0; } }

        public static List<string> GetDirectories(string path, string searchPattern = "*",
           SearchOption searchOption = SearchOption.AllDirectories)
        {
            if (searchOption == SearchOption.TopDirectoryOnly)
                return Directory.GetDirectories(path, searchPattern).ToList();

            var directories = new List<string>(GetDirectories(path, searchPattern));

            for (var i = 0; i < directories.Count; i++)
                directories.AddRange(GetDirectories(directories[i], searchPattern));

            return directories;
        }

        private static List<string> GetDirectories(string path, string searchPattern)
        {
            try
            {
                return Directory.GetDirectories(path, searchPattern).ToList();
            }
            catch (System.Exception)
            {
                return new List<string>();
            }
        }


        [MenuItem("Fiber/FiberCore/Update Resources")]
        public static void Update()
        {
            UpdateResources(Application.dataPath);
            AssetDatabase.Refresh();
        }

       
        public static void UpdateResources(string dataPath)
        {
            var path = dataPath + "/Resources";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var directories = GetDirectories(path);
            directories.Add(path + "/");

            for (int i = 0; i < directories.Count; i++)
            {
                directories[i] = directories[i].Replace(@"\", "/");
            }


            for (int i = 0; i < directories.Count; i++)
            {
                var old = directories[i];

                directories[i] = old.Replace(path + "/", "");
            };

            var resources = new List<string>();

            foreach (var item in directories)
            {
                var newpath = path + "/" + item;
                var files = Directory.GetFiles(newpath, "*", SearchOption.TopDirectoryOnly).Where(name => !name.EndsWith(".meta"));
                foreach (var file in files)
                {
                    var trimmedFile = file.Replace(@"\", "/").Replace(path + "/", "").Split('.')[0];
                    resources.Add(trimmedFile.Trim());
                }
            }


            using (FileStream fs = File.Create(path + "/ResourcesInfo.txt"))
            {
                fs.Flush();
                fs.Close();
            }


            using (StreamWriter sw = File.AppendText(dataPath + "/Resources/ResourcesInfo.txt"))
            {
                foreach (var item in resources)
                {
                    Debug.Log("<color=orange><b>[CORE.BUILD]: </b></color>Resource registered: " + item);
                    sw.WriteLine(item);
                }
            }



            Debug.Log("<color=orange><b>[CORE.BUILD]: </b></color>Resource list compilled successfully");
        }

        public void OnPreprocessBuild(BuildReport report)
        {
            Update();
        }
    }
}