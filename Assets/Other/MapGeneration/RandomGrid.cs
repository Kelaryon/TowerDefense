using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGrid : MonoBehaviour
{
    // Start is called before the first frame update
    public int[,] GridMap;
    string view;
    Vector2Int start;
    Vector2Int end;
    public List<Vector2Int> roadList;
    int roadLength;
    private int GridSize;
    int maxLength;
    
    public void GridSizeSet(int size)
    {
        GridSize = size;
    }

    public int[,] SetGrid(int a, int b)
    {
        maxLength = GridSize * 2 + 1;
        roadLength = 0;
        GridMap = new int[a, b];
        roadList = new List<Vector2Int>();
        start = RandomRoadStartAndEnd(a, b);
        do {
            end = RandomRoadStartAndEnd(a,b);
        }while (start == end);
        CreateRoad(start);
        GridMap[start[0], start[1]] = 1;
        GridMap[end[0], end[1]] = 2;
        PrintArray(GridMap);
        return GridMap;
        //Debug.Log("Distance: " + currentDistance);
    }

    public Vector2Int RandomRoadStartAndEnd(int a, int b)
    {
        int alfa = Random.Range(0, a);
        int beta;
        if (alfa > 0 && alfa < a)
        {
            if (Random.value < 0.5f)
            {
                beta = 0;
            }
            else
            {
                beta = b - 1;
            }
        }
        else
        {
            beta = Random.Range(0, b);
        }
        //GridMap[alfa, beta] = 1;
//        Debug.Log(alfa.ToString() + " " + beta.ToString());
        return new Vector2Int(alfa, beta);
    }
    public void PrintArray(int[,] arr)
    {
        view += "\n";
        for (int i = 0; i < arr.GetLength(0); i++)
        {
            for (int n = 0; n < arr.GetLength(1); n++)
            {
                view += arr[i, n].ToString();
            }
            view += "\n";
        }
        Debug.Log(view);
    }
    public int DistanceCalc(Vector2Int start)
    {
        return Mathf.FloorToInt(Mathf.Sqrt((Mathf.Pow(start[0] - end[0], 2))) + Mathf.Sqrt(Mathf.Pow(start[1] - end[1], 2)) - 1);
    }
    private bool CreateRoad(Vector2Int alfa)
    {
        roadLength++;
        MarkRoad(alfa);
        roadList.Add(alfa);
        int x = Random.Range(0, 4);
        bool[] dirCheck = new bool[4];
        byte remainingDir = 0;
        if (DistanceCalc(alfa) == 0 && ((Random.value*roadLength/maxLength) > 0.5f || roadLength == maxLength))
        {
            roadList.Add(end);
            MarkRoad(alfa);
            return true;
        }
        while (true)
        {
            if (remainingDir == 4) {
                roadList.RemoveAt(roadList.Count - 1);
                GridMap[alfa[0], alfa[1]] = 0;
                roadLength--;
                return false;
            }
            while (dirCheck[x] == true)
            {
                x = Random.Range(0, 4);
            }
            switch (x)
            {
                case 0:
//                    Debug.Log("Up");
                    if (CheckUp(alfa[0], alfa[1]))
                    {
                        if (maxLength - roadLength >= DistanceCalc(alfa + new Vector2Int(-1, 0)))
                        {
                            if (CreateRoad(alfa + new Vector2Int(-1, 0)) == true)
                            {
//                                MarkRoad(alfa);
                                return true;
                            }
                        }
                    };
                    dirCheck[0] = true;
                    remainingDir++;
                    break;
                case 1:
 //                   Debug.Log("Down");
                    if (CheckDown(alfa[0], alfa[1]))
                    {
                        if (maxLength - roadLength >= DistanceCalc(alfa + new Vector2Int(1, 0)))
                        {
                            if (CreateRoad(alfa + new Vector2Int(1, 0)) == true)
                            {
//                                MarkRoad(alfa);
                                return true;
                            }
                        }
                    };
                    dirCheck[1] = true;
                    remainingDir++;
                    break;
                case 2:
  //                  Debug.Log("Left");
                    if (CheckLeft(alfa[0], alfa[1]))
                    {
                        if (maxLength - roadLength >= DistanceCalc(alfa + new Vector2Int(0, -1)))
                        {
                            if (CreateRoad(alfa + new Vector2Int(0, -1)) == true)
                            {
//                                MarkRoad(alfa);
                                return true;
                            }
                        }
                    };
                    dirCheck[2] = true;
                    remainingDir++;
                    break;
                case 3:
//                   Debug.Log("Right");
                    if (CheckRight(alfa[0], alfa[1]))
                    {
                        if (maxLength - roadLength >= DistanceCalc(alfa + new Vector2Int(0, 1)))
                        {
                            if (CreateRoad(alfa + new Vector2Int(0, 1)) == true)
                            {
//                              MarkRoad(alfa);
                                return true;
                            }
                        }
                    };
                    dirCheck[3] = true;
                    remainingDir++;
                    break;
            }
        }
    }
    private bool CheckUp(int a, int b) {
        if (a == 0) {
            return false;
        }
        if (GridMap[a - 1, b] == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool CheckDown(int a, int b) {
        if (a == GridMap.GetLength(1) - 1)
        {
            return false;
        }
        if (GridMap[a + 1, b] == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool CheckLeft(int a, int b)
    {
        if (b == 0)
        {
            return false;
        }
        if (GridMap[a, b - 1] == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool CheckRight(int a, int b)
    {
        if (b == GridMap.GetLength(0) - 1)
        {
            return false;
        }
        if (GridMap[a, b + 1] == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void MarkRoad(Vector2Int poz)
    {
        GridMap[poz[0], poz[1]] = 5;
    }

}
