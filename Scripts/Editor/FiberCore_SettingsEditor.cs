using FiberCore.Common;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FiberCore.Editor
{
    public class FiberCore_SettingsEditor : EditorWindow
    {
        FiberCore_Settings settingsSO;
        UnityEditor.Editor editor;

        [MenuItem("Fiber/FiberCore/Settings")]
        static void Init()
        {
            var window = GetWindow(typeof(FiberCore_SettingsEditor));
            window.Show();
            window.titleContent = new GUIContent("FiberCore");
            window.maxSize = new Vector2(469, 9999);
            window.minSize = new Vector2(469, 500);

        }

        private void OnEnable()
        {
            GetConfig();
            editor = UnityEditor.Editor.CreateEditor(settingsSO);
        }

        private void GetConfig()
        {
            FiberCore_EditorFeatures.CheckFiberSettingsFile();
            settingsSO = Resources.Load<FiberCore_Settings>("FiberCoreSettings");

        }

        private void OnGUI()
        {
            ((FiberSettingsEditor)editor).Draw();
        }
    }

    [CustomEditor(typeof(FiberCore_Settings))]
    public class FiberSettingsEditor : UnityEditor.Editor
    {
        #region Styles

        private GUIStyle style          = new GUIStyle();

        private GUIStyle titleStyle;
        private GUIStyle titleTextStyle;

        private Texture2D logo;

        #endregion

        #region Properties
        private Vector2 scrollPosition;

        private List<SerializedProperty> properties_Debugging ;
        private List<SerializedProperty> properties_FPS;
        private List<SerializedProperty> properties_Pools;

        #endregion

        #region Enable

        void OnEnable()
        {
            GetProperties();

            logo = Resources.Load("Fiber") as Texture2D;

            titleStyle     = new GUIStyle();
            titleTextStyle = new GUIStyle();

            var colorTexture = new Texture2D(1, 1);
            colorTexture.SetPixel(0, 0, new Color(0.17f, 0.17f, 0.17f, 1f));
            colorTexture.Apply();

            titleStyle.normal.background = colorTexture;
            titleTextStyle.alignment = TextAnchor.MiddleCenter;
            titleTextStyle.fontStyle = FontStyle.Bold;
            titleTextStyle.fontSize = 13;
            titleTextStyle.normal.textColor = Color.gray;
            titleTextStyle.margin = new RectOffset(0, 0, 3, 0);


            colorTexture = new Texture2D(1, 1);
            colorTexture.SetPixel(0, 0, new Color(0.1f, 0.1f, 0.1f, 0.5f));
            colorTexture.Apply();

            style.normal.background = colorTexture;
            style.fixedHeight = 0;
        }

        #endregion

        private void GetProperties()
        {
            properties_Debugging = new List<SerializedProperty>()
            {
            serializedObject.FindProperty("_allowLogs"),
            serializedObject.FindProperty("_allowWarnings"),
            serializedObject.FindProperty("_allowErrors"),
            };

            properties_FPS = new List<SerializedProperty>()
            {
            serializedObject.FindProperty("_calculateFPS"),
            serializedObject.FindProperty("_limitFPS"),
            serializedObject.FindProperty("_enableVSync"),
            };

            properties_Pools = new List<SerializedProperty>()
            {
            serializedObject.FindProperty("_poolExpandMethod"),
            serializedObject.FindProperty("_poolCleanUpRate"),
            };
        }

        private void DrawLabel(string content, string type)
        {
            float offset = 0;

            switch (type)
            {
                case "bool": offset = 40; break;
                case "Enum": offset = 100; break;
                case "float": offset = 100; break;
                case "uint": offset = 100; break;
            }


            GUILayout.Label(content, GUILayout.Width(Screen.width - offset));
        }


        public void Draw()
        {
            GUI.DrawTexture(new Rect((Screen.width / 2) - logo.width / 2, 0, logo.width, logo.height), logo);
            GUILayout.Space(logo.height);

            scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false);

            var x = 0f;
            x += DrawWindow(x, properties_Debugging, "DEBUGGING");
            x += DrawWindow(x, properties_FPS, "PERFORMANCE");
            x += DrawWindow(x, properties_Pools, "POOLS");

            GUILayout.EndScrollView();
        }

        public float DrawWindow(float offset, List<SerializedProperty> properties, string title)
        {
            var spacing        = 25;
            var padding        = 1 * spacing;
            var elementHeight  = 24;
            var startPoint     = offset * spacing + padding;
            var endPoint       = properties.Count + padding / spacing;


            GUILayout.Space((elementHeight * endPoint) + endPoint);

            GUILayout.BeginArea(new Rect(0, startPoint - padding, Screen.width, elementHeight), titleStyle);

            GUILayout.Label(title, titleTextStyle, GUILayout.Width(Screen.width));

            GUILayout.EndArea();


            serializedObject.Update();

            for (int i = 0; i < properties.Count; i++)
            {
                var property = properties[i];

                var tooltip = FieldData.GetValue(property.name, typeof(FiberCore_Settings), FieldData.ValueType.Tooltip);

                GUILayout.BeginArea(new Rect(0, startPoint + i * spacing, Screen.width, elementHeight), new GUIContent("", tooltip), EditorStyles.helpBox);
                GUILayout.BeginHorizontal();


                var name = FieldData.GetValue(property.name, typeof(FiberCore_Settings), FieldData.ValueType.Description);
                
                if (name == null)
                {
                    name = property.displayName;
                }
                DrawLabel(name, property.type);
                EditorGUILayout.PropertyField(property,GUIContent.none, true, GUILayout.Width(74));

                GUILayout.EndHorizontal();
                GUILayout.EndArea();
            }

            serializedObject.ApplyModifiedProperties();

            return endPoint;
        }
    }
}
