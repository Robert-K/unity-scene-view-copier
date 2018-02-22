using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Camera)), CanEditMultipleObjects]
public class SceneViewCopier : Editor
{
    private bool follow = false;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.Space();

        EditorGUILayout.BeginVertical(GUI.skin.box);

        EditorGUILayout.LabelField(new GUIContent("Copy"));

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button(new GUIContent("Position")))
        {
            SceneView view = SceneView.lastActiveSceneView;
            if (view != null)
            {
                for (int i = 0; i < targets.Length; i++)
                {
                    Camera temp = (Camera)targets[i];
                    Undo.RecordObject(temp.transform, "Copy Scene View Position");
                    temp.transform.position = view.camera.transform.position;
                }
            }
        }

        if (GUILayout.Button(new GUIContent("Rotation")))
        {
            SceneView view = SceneView.lastActiveSceneView;
            if (view != null)
            {
                for (int i = 0; i < targets.Length; i++)
                {
                    Camera temp = (Camera)targets[i];
                    Undo.RecordObject(temp.transform, "Copy Scene View Rotation");
                    temp.transform.rotation = view.camera.transform.rotation;
                }
            }
        }

        if (GUILayout.Button(new GUIContent("Settings")))
        {
            SceneView view = SceneView.lastActiveSceneView;
            if (view != null)
            {
                for (int i = 0; i < targets.Length; i++)
                {
                    Camera temp = (Camera)targets[i];
                    Undo.RecordObject(temp, "Copy Scene View Settings");
                    temp.fieldOfView = view.camera.fieldOfView;
                    temp.orthographic = view.camera.orthographic;
                    temp.orthographicSize = view.camera.orthographicSize;
                    temp.nearClipPlane = view.camera.nearClipPlane;
                    temp.farClipPlane = view.camera.farClipPlane;
                }
            }
        }

        EditorGUILayout.EndHorizontal();

        EditorGUI.BeginChangeCheck();
        follow = EditorGUILayout.Toggle(new GUIContent("Follow"), follow);
        if (EditorGUI.EndChangeCheck())
        {
            Tools.hidden = follow;
            SceneView.RepaintAll();
        }

        EditorGUILayout.EndVertical();
    }

    private void OnSceneGUI()
    {
        if (follow)
        {
            SceneView view = SceneView.lastActiveSceneView;
            if (view != null)
            {
                Camera temp = (Camera)target;
                Undo.RecordObject(temp.transform, "Follow Scene View");
                temp.transform.position = view.camera.transform.position;
                temp.transform.rotation = view.camera.transform.rotation;
            }
        }
    }

    private void OnDisable()
    {
        Tools.hidden = false;
    }
}
