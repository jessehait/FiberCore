using UnityEditor.Build;
using UnityEditor.Build.Reporting;


namespace FiberCore.Editor
{
    public class FiberCore_BuildPreprocess : IPreprocessBuildWithReport
    {
        public int callbackOrder { get { return 0; } }

        public void OnPreprocessBuild(BuildReport report)
        {
            FiberCore_EditorFeatures.CheckResourcesManifest();
        }
    }
}