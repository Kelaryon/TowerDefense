using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;

[ExecuteInEditMode]
public class Waypoint : MonoBehaviour,ISelectableInterface
{
    [SerializeField] bool isPlaceble;
    [HideInInspector]
    public Sprite icon;
    [HideInInspector]
    public Bank tControl;
    int gridXPoz;
    int gridZPoz;
    public List<GameObject> randomEnviromentObjectList;
    //[HideInInspector]
    [SerializeField] GameObject enviromentObject;
    public string[] enviromentalObjectsName;
    //public List<string> enviromentalObjectsName; 
    [HideInInspector]
    public int enviromentalObjectIndex;

    private void Start()
    {
        enviromentalObjectsName = new string[randomEnviromentObjectList.Count];
        //foreach(GameObject gobj in randomEnviromentObjectList)
        //{
        //    //enviromentalObjectsName.Add(gobj.name);
        //}
        for(int i = 0; i < randomEnviromentObjectList.Count; i++)
        {
            enviromentalObjectsName[i] = randomEnviromentObjectList[i].name;
        }
        //ReloadEnviromentPrefab();
    }

    public void SetEnviroment()
    {
        if (enviromentObject != null)
        {
            DestroyImmediate(enviromentObject.gameObject);
        }
        enviromentObject = Instantiate(randomEnviromentObjectList[enviromentalObjectIndex], transform.position, Quaternion.Euler(0, Random.Range(0.0f, 360.0f),0));
        enviromentObject.transform.SetParent(this.transform);
    }

    public void ReloadEnviromentPrefab()
    {
        DestroyImmediate(enviromentObject.gameObject);
        enviromentObject = Instantiate(randomEnviromentObjectList[enviromentalObjectIndex], transform.position, Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0));
        enviromentObject.transform.SetParent(this.transform);
    }

    //[Range(0.0f, 1.0f)]
    //public float enviromentGenerated;

    //private void Start()
    //{
    //    if (randomEnviromentObjectList.Count != 0)
    //    {
    //        if (Random.value < enviromentGenerated)
    //        {
    //            enviromentObject = Instantiate(randomEnviromentObjectList[0], this.transform.position, Quaternion.identity);
    //            enviromentObject.transform.parent = this.transform;
    //        }
    //    }
    //}
    public bool IsPlaceble { get { return isPlaceble;}}
    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (isPlaceble)
        {
            tControl = FindObjectOfType<Bank>();
            if (tControl.towerPrefab != null)
            {
                if (enviromentObject != null)
                {
                    Destroy(enviromentObject.gameObject);
                }
                bool isPlaced = tControl.towerPrefab.CreateTower(tControl.towerPrefab, transform.position, tControl, this);
                isPlaceble = !isPlaced;

            }
        }
    }

    public SelectedInfo GetInfo()
    {
        return new SelectedInfo(new Dictionary<string, string>{
            { "Status", isPlaceble.ToString()},
            { "FlavourText", "Just a basic tile" }
            }
        , icon);
    }
    
    public void SetPlaceble()
    {
        isPlaceble = !isPlaceble;
    }
}
[ExecuteInEditMode]
[CustomEditor(typeof(Waypoint))]
public class WaypointCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        DrawDefaultInspector();

        Waypoint script = (Waypoint)target;
        script.enviromentalObjectIndex = EditorGUILayout.Popup("EnviromentalObjects", script.enviromentalObjectIndex, script.enviromentalObjectsName);
        //if (EditorGUI.EndChangeCheck())
        //{
        //    script.SetEnviromet();
        //}

        if (GUILayout.Button("Build Object") && script.IsPlaceble)
        {
            script.SetEnviroment();
        }
    }
}
