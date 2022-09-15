using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class Waypoint : MonoBehaviour,ISelectableInterface
{
    [SerializeField] private bool isPlaceble;
    public GameObject enviromentObject;
    [HideInInspector]
    private Sprite icon;
    private Bank bank;
    private Structure buildStructure;
    //[SerializeField] private TileEnviromentScript tile;
    [SerializeField] private MapReferenceScript mapRef;

    private void Start()
    {
        bank = mapRef.GetBank();
        SetGridTile();
    }

    public bool IsPlaceble { get { return isPlaceble; } }
    public Structure GetStructure { get { return buildStructure; } }

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

    void SetGridTile()
    {
        bank.gridMap.SetValue(this.transform.position,this);
    }
    public void SetStructure(Structure structure)
    {
        buildStructure = structure;
    }

    public void DestroyEnviromentObject()
    {
        if (enviromentObject != null)
        {
            Destroy(enviromentObject);
        }
    }

    public UnityAction[] GetActionList()
    {
        return null;
    }
    public virtual ISelectableInterface GetSelected()
    {
        if (buildStructure != null)
        {
            return buildStructure.GetSelected();
        }
        return this;
    }
    //Null for the Moment
    public void Deactivate()
    {
    }
    public void Activate()
    {
    }
}