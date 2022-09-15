using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestScript : MonoBehaviour
{
    private GridMapScript<Waypoint> grid;
    [SerializeField] List<Waypoint> tileList;
    Waypoint selectedTile;
    //False for placing and true for selecting
    bool placeOrSelect;
    // Start is called before the first frame update
    void Start()
    {
        placeOrSelect = false;
        grid = new GridMapScript<Waypoint>(10, 10, 10);
    }

    public void ChangeTile(int index)
    {
        selectedTile = tileList[index];
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            if (placeOrSelect == false)
            {
                grid.GetCell(Utils.GetMouseWorldPoint(), out int x, out int z);
                if (grid.CheckIfInGrid(x, z))
                {
                    Waypoint way = grid.GetCellValue(x, z);
                    if (way == null && selectedTile != null)
                    {
                        way = Instantiate(selectedTile, grid.GetCellWorldPosition(x, z) + grid.CellSizeDifference(), Quaternion.identity);
                        grid.SetValue(Utils.GetMouseWorldPoint(), way);
                    }
                    else
                    {
                        Debug.Log("Position is filled");
                    }
                }
                else
                {
                    Debug.Log("Out of bounds");
                }
            }
        }
    }
}
