using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Waypoint : MonoBehaviour,ISelectableInterface
{
    [SerializeField] bool isPlaceble;
    public Sprite icon;
    public Bank tControl;
    public List<GameObject> randomEnviromentObjectList;
    private GameObject enviromentObject;
    [Range(0.0f, 1.0f)]
    public float enviromentGenerated;

    private void Start()
    {
        if (randomEnviromentObjectList.Count != 0)
        {
            if (Random.value < enviromentGenerated)
            {
                enviromentObject = Instantiate(randomEnviromentObjectList[0], this.transform.position, Quaternion.identity);
                enviromentObject.transform.parent = this.transform;
            }
        }
    }
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
                    Destroy(enviromentObject);
                }
                bool isPlaced = tControl.towerPrefab.CreateTower(tControl.towerPrefab, transform.position,tControl,this);
                isPlaceble = !isPlaced;

            }
        }
    }

    public Dictionary<string, string> GetInfo()
    {
        Dictionary<string, string> detailList = new Dictionary<string, string>
        {
            { "Status", isPlaceble.ToString()},
            { "FlavourText", "Just a basic tile" }
    };
        return detailList;
    }
    public void SetPlaceble()
    {
        isPlaceble = !isPlaceble;
    }
}
