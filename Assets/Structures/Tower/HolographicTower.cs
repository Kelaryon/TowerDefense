using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HolographicTower : MonoBehaviour
{
    //public MeshRenderer meshRenderer;
    private Bank bank;
    private Renderer[] render;
    delegate bool TowerCheckDelegate(Vector3 wayPos, Bank bank);

    private void Awake()
    {
        render = GetComponentsInChildren<Renderer>();
    }
    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, CellToWorld(bank.gridMap.CellGridPosition(Utils.GetMouseWorldPoint(),new Vector2Int(bank.buildingPrefab.GetLength(), bank.buildingPrefab.GetWidth()))), Time.deltaTime * 20f);
        if (transform.hasChanged && !EventSystem.current.IsPointerOverGameObject())
        {
            ChangeColorOnInvalid(Utils.GetMouseWorldPoint(), bank);
            transform.hasChanged = false;
        }
    }

    public void Setup(Bank bank)
    {
        this.bank = bank;
    }
    private Vector3 CellToWorld(Vector3Int cellPos)
    {
        return bank.gridMap.GetCellWorldPosition(cellPos.x, cellPos.z) + bank.gridMap.CellSizeDifference();
    }
    //private void ChangeColorOnInvalid(Vector3 wayPos)
    //{
    //    List<Waypoint> wayList = bank.gridMap.GetCellList(bank.gridMap.GetCellList(wayPos, bank.towerPrefab.GetLength(), bank.towerPrefab.GetWidth()));
    //    foreach (var wayCell in wayList)
    //    {
    //        if (wayCell.IsPlaceble == false)
    //        {
    //            foreach (Renderer r in render)
    //            {
    //                r.material.color = Color.red;
    //            }
    //            bank.canBuild = false;
    //            return;
    //        }
    //    }
    //    foreach (Renderer r in render)
    //    {
    //        r.material.color = new Color32(0, 213, 255, 197);
    //    }
    //    bank.canBuild = true;
    //}
    private void ChangeColorOnInvalid(Vector3 wayPos,Bank bank)
    {

        if (bank.buildingPrefab.BuildCondition(wayPos,bank))
        {
            foreach (Renderer r in render)
            {
                r.material.color = new Color32(0, 213, 255, 197);
            }
            bank.canBuild = true;
        }
        else
        {
            foreach (Renderer r in render)
            {
                r.material.color = Color.red;
            }
            bank.canBuild = false;
        }

    }
}
