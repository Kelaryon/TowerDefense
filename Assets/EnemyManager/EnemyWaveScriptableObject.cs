using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif


[CreateAssetMenu(menuName ="EnemyManagement/Waves")]
[System.Serializable]
public class EnemyWaveScriptableObject : ScriptableObject
{
    [HideInInspector]
    public readonly string[] enemyName = { "A","B"};
    [HideInInspector]
    public List<int> enemyList;
    //public List<string> enemyListName;
    public float timeBetweenEnemies;
    public float timeBetweenWaves;

    public void DebuggUI()
    {
        for(int i = 0; i < enemyList.Count; i++)
        {
            Debug.Log("Position " + i+":" + enemyList[i]);
        }
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(EnemyWaveScriptableObject))]
public class WaveEditor : Editor
{
    // This will be the serialized clone property of EnemyWaveScriptableObject.enemyList
    private SerializedProperty EnemyList;

    //The reordeble List
    private ReorderableList enemyList;

    //The scriptable object reference
    private EnemyWaveScriptableObject Wave;

    private GUIContent[] availableOptions;

    private void OnEnable()
    {
        Wave = (EnemyWaveScriptableObject)target;
        //availableOptions = Wave.enemyName.Select(item => new GUIContent(item)).ToArray();
        //EnemyName = serializedObject.FindProperty(nameof(EnemyWaveScriptableObject.enemyName));
        EnemyList = serializedObject.FindProperty(nameof(EnemyWaveScriptableObject.enemyList));

        enemyList = new ReorderableList(serializedObject, EnemyList)
        {
            displayAdd = true, //for adding
            displayRemove = true, //for removing
            draggable = true, // for re-ordering

            // As the header we simply want to see the usual display name of the DialogueItems
            drawHeaderCallback = rect => EditorGUI.LabelField(rect, EnemyList.displayName),

            drawElementCallback = (rect, index, focused, active) =>
            {
                // get the current element's SerializedProperty
                var element = EnemyList.GetArrayElementAtIndex(index);

                //the height of the popout panel directly related to the element;
                var popUpHeight = EditorGUI.GetPropertyHeight(element);

                //Save the previrious color
                var color = GUI.color;

                // if the value is invalid tint the next field red
                if (element.intValue < 0) GUI.color = Color.red;


                //The popout for all the elements in the list
                element.intValue = EditorGUI.Popup(new Rect(rect.x, rect.y, rect.width, popUpHeight), new GUIContent(element.displayName),element.intValue, availableOptions);

                //Change the color back to normal for the rest 
                GUI.color = color;
            },
            onAddCallback = list =>
            {
                // This adds the new element but copies all values of the select or last element in the list
                list.serializedProperty.arraySize++;

                //This sets the value of the last element to a nonexisting value
                var newElement = list.serializedProperty.GetArrayElementAtIndex(list.serializedProperty.arraySize - 1);
                newElement.intValue = -1;

            }


        };
        availableOptions = Wave.enemyName.Select(item => new GUIContent(item)).ToArray();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DrawScriptField();
        serializedObject.Update();
        EditorGUI.BeginChangeCheck();
        enemyList.DoLayoutList();
        if (EditorGUI.EndChangeCheck())
        {
            // Write back changed values into the real target
            serializedObject.ApplyModifiedProperties();
            availableOptions = Wave.enemyName.Select(item => new GUIContent(item)).ToArray();
        }

        serializedObject.ApplyModifiedProperties();

        if (GUILayout.Button("Build Object") )
        {
            Wave.DebuggUI();
        }
    }

    private void DrawScriptField()
    {
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.ObjectField("Script", MonoScript.FromScriptableObject((EnemyWaveScriptableObject)target), typeof(EnemyWaveScriptableObject), false);
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.Space();
    }


}
#endif