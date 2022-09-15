using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GridMapConvert : MonoBehaviour
{
    [SerializeField] GameObject path;
    [SerializeField] GameObject enviro;    
    public void Realocation()
    {
        foreach (Transform child in path.transform)
        {
            child.position += new Vector3(5, 0, 5);
        }
        foreach (Transform child in enviro.transform)
        {
            child.position += new Vector3(5, 0, 5);
        }
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(GridMapConvert))]
[CanEditMultipleObjects]
public class GridMapConvertEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GridMapConvert mapConvert = (GridMapConvert)target;
        var alf1 = GUILayout.Button("Relocation");
        var alf2 = GUILayout.Button("Versionchange");
        var alf3 = GUILayout.Button("Map Debug");
        if (alf1)
        {
            mapConvert.Realocation();
        }
    }
}
#endif