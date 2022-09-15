using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtractorScript : Building
{
    public EnviromentalBuilding resourceNode;
    //public override bool BuildCondition(Vector3 wayPos, Bank bank)
    //{
    //    bank.gridMap.GetCell(Utils.GetMouseWorldPoint(), out int x, out int z);
    //    Waypoint way = bank.gridMap.GetCellValue(x, z);
    //    if (way.IsPlaceble == false)
    //    {
    //        return false;
    //    }
    //    if (bank.gridMap.CheckIfInGrid(x + 1, z) && (bank.gridMap.GetCellValue(x + 1, z).buildStructure is EnviromentalBuilding))
    //    {
    //        return true;
    //    }
    //    if (bank.gridMap.CheckIfInGrid(x - 1, z) && (bank.gridMap.GetCellValue(x - 1, z).buildStructure is EnviromentalBuilding))
    //    {
    //        return true;
    //    }
    //    if (bank.gridMap.CheckIfInGrid(x, z + 1) && (bank.gridMap.GetCellValue(x, z + 1).buildStructure is EnviromentalBuilding))
    //    {
    //        return true;
    //    }
    //    if (bank.gridMap.CheckIfInGrid(x, z - 1) && (bank.gridMap.GetCellValue(x, z - 1).buildStructure is EnviromentalBuilding))
    //    {

    //        return true;
    //    }
    //    return false;
    //}
    public override bool BuildCondition(Vector3 wayPos, Bank bank)
    {
        bank.gridMap.GetCell(Utils.GetMouseWorldPoint(), out int x, out int z);
        Waypoint way = bank.gridMap.GetCellValue(x, z);
        if (way.GetStructure is EnviromentalBuilding)
        {
            return true;
        }
        return false;
    }
}
