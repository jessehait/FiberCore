using UnityEditor;
using UnityEngine;

public class FiberCore_SettingsEditor : EditorWindow
{
     FiberCoreSettings settingsSO;
     Editor            editor;

    [MenuItem("Fiber/FiberCore/Settings")]
    static void Init()
    {
        var window = GetWindow(typeof(FiberCore_SettingsEditor));
        window.Show();
    }

    private void OnEnable()
    {
        settingsSO = Resources.Load<FiberCoreSettings>("FiberCoreSettings");
        editor     = Editor.CreateEditor(settingsSO);
    }

    private void OnGUI()
    {
        editor.OnInspectorGUI();
    }
}

[CustomEditor(typeof(FiberCoreSettings))]
public class FiberSettingsEditor: Editor
{
    SerializedProperty[] properties;

    private void GetProperties()
    {
        properties = new SerializedProperty[]
        {
            serializedObject.FindProperty("_allowLogs"),
            serializedObject.FindProperty("_allowWarnings"),
            serializedObject.FindProperty("_allowErrors"),
            serializedObject.FindProperty("_autoUpdateResourceList"),
        };
    }


    void OnEnable()
    {
        GetProperties();
        SetStyle();
    }

    GUIStyle style = new GUIStyle();

    private void SetStyle()
    {
        var colorTexture = new Texture2D(1, 1);
        colorTexture.SetPixel(0, 0, new Color(0.3f, 0.3f, 0.3f, 1));
        colorTexture.Apply();

        style.normal.background = colorTexture;
    }


    public override void OnInspectorGUI()
    {

        serializedObject.Update();

        foreach (var item in properties)
        {

            GUILayout.BeginHorizontal(style);
            GUILayout.Label(item.displayName, GUILayout.Width(Screen.width - 30));
            GUILayout.FlexibleSpace();
            EditorGUILayout.PropertyField(item, GUIContent.none, true, GUILayout.MinWidth(100));
            GUILayout.EndHorizontal();
            GUILayout.Space(3);


        }

       
        serializedObject.ApplyModifiedProperties();
    }
}
