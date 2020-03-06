#if UNITY_EDITOR
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Web;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

class RHCore_BuildPreprocess : IPreprocessBuildWithReport
{
    public int callbackOrder { get { return 0; } }

    [MenuItem("RHCore/Update Resources")]
    public static void UpdateResources()
    {
        var path = Application.dataPath + "/Resources";

        if (!System.IO.Directory.Exists(path))
            System.IO.Directory.CreateDirectory(path);

        var directories = Directory.GetDirectories(path + "/").ToList();

        for (int i = 0; i < directories.Count; i++)
        {
            var old = directories[i];

            directories[i] = old.Replace(path + "/", "");
        };

        var resources = new List<string>();

        foreach (var item in directories)
        {
            var itemsInFolder = Resources.LoadAll(item);

            foreach (var asset in itemsInFolder)
            {
                resources.Add(item + "/" + asset.name);
            }
        }

        using (FileStream fs = File.Create(path + "/ResourcesInfo.txt"))
        {
            fs.Flush();
            fs.Close();
        }


        using (StreamWriter sw = File.AppendText(Application.dataPath + "/Resources/ResourcesInfo.txt"))
        {
            foreach (var item in resources)
            {
                Debug.Log("<color=orange>[CORE.BUILD]</color>Resource registered: " + item);
                sw.WriteLine(item);
            }
        }

        AssetDatabase.Refresh();

        Debug.Log("<color=orange>[CORE.BUILD]</color>Resource list compilled successfully");
    }

    public void OnPreprocessBuild(BuildReport report)
    {
        UpdateResources();
    }
}
#endif