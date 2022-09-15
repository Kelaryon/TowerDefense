using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMapScript<T>
{
    private readonly int sizeX;
    private readonly int sizeZ;
    private readonly float cellSize;
    private T[,] gridMap;

    public GridMapScript(int sizeX, int sizeZ, float cellSize)
    {
        this.sizeX = sizeX;
        this.sizeZ = sizeZ;
        this.cellSize = cellSize;

        gridMap = new T[sizeX, sizeZ];

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

    //Difference is includet manually
    public Vector3 CellSizeDifference()
    {
        return new Vector3(cellSize*.5f,0,cellSize*.5f);
    }
    public void GetCell(Vector3 worldPoint, out int x, out int z)
    {
        x = Mathf.FloorToInt(worldPoint.x / cellSize);
        z = Mathf.FloorToInt(worldPoint.z / cellSize);
    }
    public Vector3Int CellGridPosition(Vector3 poz, Vector2Int prefabSize)
    {
        GetCell(poz, out int x, out int z);

        //clamp here
        x = Mathf.Clamp(x, 0, ReturnSizeX() - prefabSize.x);
        z = Mathf.Clamp(z, 0, ReturnSizeZ() - prefabSize.y);


        return new Vector3Int(x, 0, z);
    }
    public List<Vector3Int> GetCellList(Vector3 initPoz,int length, int width)
    {
        Vector3Int cellPos = CellGridPosition(initPoz, new Vector2Int(length, width));
        List<Vector3Int> cellList = new List<Vector3Int>();
        for(int i = 0; i< length; i++)
        {
            for(int k = 0; k < width; k++)
            {
                cellList.Add(new Vector3Int(cellPos.x + i,0,cellPos.z + k));
            }
        }
        return cellList;
    }
    public List<T> GetCellList(List<Vector3Int> list)
    {
        List<T> cellList = new List<T>();
        foreach (Vector3Int pos in list)
        {
                cellList.Add(GetCellValue(pos.x, pos.z));
        }
        return cellList;
    }
    public T GetCellValue(int x, int z)
    {
        return gridMap[x, z];
    }
    public void SetValue(Vector3 position, T tile)
    {
        GetCell(position, out int x, out int z);
        gridMap[x, z] = tile;
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
    public int ReturnSizeX()
    {
        return sizeX;
    }
    public int ReturnSizeZ()
    {
        return sizeZ;
    }
}
