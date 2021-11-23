using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DemoObjectEditorWindow : EditorWindow
{
    Vector2 ScrollPosition;

    [MenuItem("Debug/Demo Objects")]
    public static void ShowWindow()
    {
        var window = EditorWindow.GetWindow<DemoObjectEditorWindow>();
        window.titleContent = new GUIContent("Demo Object Debugger");
    }

    private void OnInspectorUpdate()
    {
        Repaint();
    }

    private void OnGUI()
    {
        // are we currently playing?
        if (!Application.isPlaying)
        {
            GUILayout.Label("Only available in play mode");
            return;
        }
        
        // no debug interface present?
        if (DemoObjectDebugInterface.Instance == null)
        {
            GUILayout.Label("No DemoObjectDebugInterface in scene");
            return;
        }

        // no demo objects?
        if (DemoObjectDebugInterface.Instance.AllDemoObjects.Count == 0)
        {
            GUILayout.Label("No DemoObject in scene");
            return;
        }

        ScrollPosition = GUILayout.BeginScrollView(ScrollPosition);

        // Display all demo objects
        for(int index = 0; index < DemoObjectDebugInterface.Instance.AllDemoObjects.Count; index++)
        {
            var demoObject = DemoObjectDebugInterface.Instance.AllDemoObjects[index];

            if (index > 0)
                EditorGUILayout.Separator();

            // check if selected
            bool isSelected = Selection.Contains(demoObject.gameObject.GetInstanceID());
          
            demoObject.IsExpanded = EditorGUILayout.BeginFoldoutHeaderGroup(demoObject.IsExpanded, $"{(isSelected ? "*" : "")}{(index + 1)}: {demoObject.name}");
            
            // show full info?
            if (demoObject.IsExpanded)
            {
                using (new GUILayout.HorizontalScope())
                {
                    GUILayout.Space(10f);

                    using (new GUILayout.VerticalScope())
                    {
                        GUILayout.Label(demoObject.GetDebugInfo());
                    }
                }

                using (new GUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("Go Slower"))
                    {
                        demoObject.GoSlower();
                    }

                    if (GUILayout.Button("Go Faster"))
                    {
                        demoObject.GoFaster();
                    }

                    // object selection requested?
                    if (!isSelected && GUILayout.Button("Select in hierarchy"))
                    {
                        Selection.activeGameObject = demoObject.gameObject;
                    }
                }
            }

            EditorGUI.EndFoldoutHeaderGroup();
        }

        GUILayout.EndScrollView();
    }
}
