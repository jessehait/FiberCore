#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.PackageManager.Requests;
using UnityEditor.PackageManager;

namespace RHGameCore.Editor
{
    public static class RHCore_PackageUpdater
    {
        static AddRequest Request;

        [MenuItem("RHCore/Update")]
        static void Add()
        {
            // Add a package to the Project
            Request = Client.Add(@"C:\Users\pcsin\Home\Work\Unity\RHCore\Assets\RHCore");
            EditorApplication.update += Progress;
        }

        static void Progress()
        {
            if (Request.IsCompleted)
            {
                if (Request.Status == StatusCode.Success)
                    Debug.Log("Core updated: " + Request.Result.packageId);
                else if (Request.Status >= StatusCode.Failure)
                    Debug.Log("Core update failed: " + Request.Error.message);

                EditorApplication.update -= Progress;
            }
        }
    }
}
#endif
