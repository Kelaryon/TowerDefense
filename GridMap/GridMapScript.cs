using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMapScript
{
    private readonly int sizeX;
    private readonly int sizeZ;
    private readonly float cellSize;
    private Waypoint[,] gridMap;

    public GridMapScript(int sizeX, int sizeZ, float cellSize)
    {
        this.sizeX = sizeX;
        this.sizeZ = sizeZ;
        this.cellSize = cellSize;

        gridMap = new Waypoint[sizeX, sizeZ];

        for(int x = 0; x < gridMap.GetLength(0); x++)
        {
            for(int z = 0; z< gridMap.GetLength(1); z++)
            {
                Debug.DrawLine(GetCellWorldPosition(x, z), GetCellWorldPosition(x, z + 1),Color.white,100f);
                Debug.DrawLine(GetCellWorldPosition(x, z), GetCellWorldPosition(x + 1, z),Color.white,100f);
            }
        }
        Debug.DrawLine(GetCellWorldPosition(0, sizeZ), GetCellWorldPosition(sizeX,sizeZ), Color.white, 100f);
        Debug.DrawLine(GetCellWorldPosition(sizeX, 0), GetCellWorldPosition(sizeX, sizeZ), Color.white, 100f);
    }

    public Vector3 GetCellWorldPosition(int x, int z)
    {
        return new Vector3(x,0,z) * cellSize;
    }
    public Vector3 CellSizeDifference()
    {
        return new Vector3(cellSize*.5f,0,cellSize*.5f);
    }
    public void GetCell(Vector3 worldPoint, out int x, out int z)
    {
        x = Mathf.FloorToInt(worldPoint.x / cellSize);
        z = Mathf.FloorToInt(worldPoint.z / cellSize);
    }

    public Waypoint GetCellValue(int x, int z)
    {
        return gridMap[x, z];
    }
    public void SetValue(Vector3 position, Waypoint tile)
    {
        GetCell(position, out int x, out int z);
        gridMap[x, z] = tile;
        Debug.Log(gridMap[x, z]);
        Debug.Log(position);

    }

    public bool CheckIfInGrid(int x, int z)
    {
        if (x >= 0 && x < sizeX && z >= 0 && z < sizeZ) {
            return true;
        }
        else
        {
            return false;
        }
    }
}
