using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Structure : MonoBehaviour,ISelectableInterface
{
    [SerializeField] private int length;
    [SerializeField] private int width;

    public UnityAction[] GetActionList()
    {
        return null;
    }

    public virtual SelectedInfo GetInfo()
    {
        return null; 
    }

    public int GetLength()
    {
        return length;
    }
    public int GetWidth()
    {
        return width;
    }
    public virtual bool BuildCondition(Vector3 wayPos, Bank bank)
    {
        //Old
        //List<Waypoint> wayList = bank.gridMap.GetCellList(bank.gridMap.GetCellList(wayPos, bank.towerPrefab.GetLength(), bank.towerPrefab.GetWidth()));
        List<Waypoint> wayList = bank.gridMap.GetCellList(bank.gridMap.GetCellList(wayPos,GetLength(),GetWidth()));
        foreach (var wayCell in wayList)
        {
            if (wayCell.IsPlaceble == false)
            {
                return false;
            }
        }
        return true;
    }

    public virtual void ActivateRangeCircle(bool value)
    {
    }

    public virtual ISelectableInterface GetSelected()
    {
        return this;
    }
    // Null for the moment
    public virtual void Deactivate()
    {
    }
    public virtual void Activate()
    {
    }
}
