using UnityEngine;
using UnityEditor;


//Do I sill want to keep this method here ?
//For the moment yes. it's a bother to change
public class TileEnviromentScript : MonoBehaviour
{
    public string[] enviromentalObjectsName;
    [SerializeField] EnviromentalScript enviromentalOS;
    [HideInInspector]
    public int enviromentalObjectIndex;
    [SerializeField] Waypoint way;

    private void Start()
    {
        enviromentalObjectsName = new string[enviromentalOS.ReturnEnviroList().Count];
        for (int i = 0; i < enviromentalOS.ReturnEnviroList().Count; i++)
        {
            enviromentalObjectsName[i] = enviromentalOS.ReturnEnviroList()[i].ToString();
        }
    }
    public void SetEnviroment()
    {
        if (way.enviromentObject != null)
        {
            DestroyImmediate(way.enviromentObject);
        }
        way.enviromentObject = Instantiate(enviromentalOS.ReturnEnviroList()[enviromentalObjectIndex], transform.position, Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0));
        way.enviromentObject.transform.SetParent(this.transform);
    }

    public Waypoint ReturnWaypoint()
    {
        return way;
    }
}

#if UNITY_EDITOR

[ExecuteInEditMode]
[CustomEditor(typeof(TileEnviromentScript))]
public class WaypointCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        DrawDefaultInspector();

        TileEnviromentScript script = (TileEnviromentScript)target;
        script.enviromentalObjectIndex = EditorGUILayout.Popup("EnviromentalObjects", script.enviromentalObjectIndex, script.enviromentalObjectsName);
        //if (EditorGUI.EndChangeCheck())
        //{
        //    script.SetEnviromet();
        //}

        if (GUILayout.Button("Build Object") && script.ReturnWaypoint().IsPlaceble)
        {
            script.SetEnviroment();
        }
    }
}
#endif
