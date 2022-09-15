using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class MapDataScript : ScriptableObject
{
    [SerializeField] int sizeX;
    [SerializeField] int sizeY;
    [SerializeField] int startBallance;
    float cellSize = 10;

    public int GetStartBallance()
    {
        return startBallance;
    }

    public void ReturnMapData(out int sizeX,out int sizeY, out float cellSize)
    {
        sizeX = this.sizeX;
        sizeY = this.sizeY;
        cellSize = this.cellSize;
    }


}
