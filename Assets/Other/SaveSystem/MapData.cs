using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapData
{
    private byte length;
    private byte width;
    private int[,] gridMap;
    int[] x;
    int[] y;
//    private List<Vector2Int> roadList;

    public void SetRoadVector(List<Vector2Int> roadList)
    {
        x = new int[roadList.Count];
        y = new int[roadList.Count];
        for(int i = 0; i < roadList.Count; i++)
        {
            x[i] = roadList[i][0];
            y[i] = roadList[i][1];
        }

    }
    public List<Vector2Int> RestoreRoadVector()
    {
        List<Vector2Int> roadList = new List<Vector2Int>();
        for(int i = 0; i < x.Length; i++)
        {
            roadList.Add(new Vector2Int(x[i], y[i]));
        }
        return roadList;
    }
    //public void SetRoadList(List<Vector2Int> roadList)
    //{
    //    this.roadList = roadList;
    //}
    //public List<Vector2Int> GetRoadList()
    //{
    //    return roadList;
    //}
    public void SetGridMap(int[,] gridMap)
    {
        this.gridMap = gridMap;
    }
    public void SetLength(byte length)
    {
        this.length = length;
    }
    public void SetWidth(byte width)
    {
        this.width = width;
    }
    public byte GetLenght()
    {
        return length;
    }
    public byte GetWidth()
    {
        return width;
    }
    public int[,] GetGridMap()
    {
        return gridMap;
    }

}
