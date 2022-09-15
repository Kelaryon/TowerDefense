using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Structure
{
    [SerializeField] protected int cost;
    protected List<Waypoint> waypointList;
    protected Bank bank;
    public virtual bool CreateBuilding(Building buildingPrefab, Vector3 position, Bank bank, List<Waypoint> waypointList)
    {
        if (bank == null)
        {
            return false;
        }

        if (bank.CurrentBallance >= cost)
        {
            foreach (Waypoint way in waypointList)
            {
                way.DestroyEnviromentObject();
                way.SetPlaceble();
            }
            //Don't forget to set the variable values to the tower pointer not the tower itself
            Building building = Instantiate(buildingPrefab, position, Quaternion.identity);
            building.Setup(waypointList, bank);
            return true;

        }
        return false;
    }
    //public bool CreateTower(Tower towerPrefab, Vector3 position, Bank bank, List<Waypoint> waypointList)
    //{
    //    if (bank == null)
    //    {
    //        return false;
    //    }

    //    if (bank.CurrentBallance >= cost)
    //    {
    //        foreach (Waypoint way in waypointList)
    //        {
    //            way.DestroyEnviromentObject();
    //            way.SetPlaceble();
    //        }
    //        //Don't forget to set the variable values to the tower pointer not the tower itself
    //        Tower building = Instantiate(towerPrefab, position, Quaternion.identity);
    //        building.Setup(waypointList, bank);
    //        return true;

    //    }
    //    return false;
    //}
    public virtual void Setup(List<Waypoint> wayList, Bank bank)
    {
        this.waypointList = wayList;
        this.bank = bank;
        foreach (Waypoint way in waypointList)
        {
            way.SetStructure(this);
        }
        this.bank.Withdraw(cost);
    }
    public Bank GetBank()
    {
        return bank;
    }
    public List<Waypoint> GetWaypoints()
    {
        return waypointList;
    }

}
