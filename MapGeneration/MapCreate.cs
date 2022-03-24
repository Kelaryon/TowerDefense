using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreate : MonoBehaviour
{
    // Start is called before the first frame update
    RandomGrid randomGrid;
    public GameObject simpleTile;
    public GameObject straightRoadTile;
    public GameObject cornerRoadTile;
    public int size;
    enum Dirrection {Up,Right,Down,Left};
    Dirrection lastPos;
    Dirrection nextPos;
    GameObject path;
    GameObject worldTile;
    public GameObject enviroment;
    GameObject road;
    GameObject pill;
    GameObject cube;

    private void Start()
    {
        randomGrid = gameObject.AddComponent<RandomGrid>();
    }
    public void GenMap()
    {
        //Delete old map on generating another
        if (enviroment.transform.childCount > 0)
        {
            foreach (Transform child in enviroment.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
        path = new GameObject("Path");
        worldTile = new GameObject("WorldTile");      
        path.transform.parent = enviroment.transform;
        worldTile.transform.parent = enviroment.transform;
        //Matrix Generation;
        randomGrid.GridSizeSet(size); // For the square matrix
        randomGrid.SetGrid(size,size); // for the paralelipipedic matrix
        // Pill(start) and Cube(end) instantiation on map generation and movemet on other generations
        if (pill != null) {
            pill.transform.position = new Vector3(randomGrid.roadList[0][0] * 10, 0, randomGrid.roadList[0][1] * 10);
            cube.transform.position = new Vector3(randomGrid.roadList[randomGrid.roadList.Count - 1][0] * 10, 0, randomGrid.roadList[randomGrid.roadList.Count - 1][1] * 10);
        }
        else
        {
            pill = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            pill.transform.position = new Vector3(randomGrid.roadList[0][0] * 10, 0, randomGrid.roadList[0][1] * 10);
            cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = new Vector3(randomGrid.roadList[randomGrid.roadList.Count - 1][0] * 10, 0, randomGrid.roadList[randomGrid.roadList.Count - 1][1] * 10);
        }
        for (int i = 0; i < size;i++)
        {
            for(int k = 0; k < size; k++)
            {
                if (randomGrid.GridMap[i, k] == 0)
                {
                    var ground = Instantiate(simpleTile,new Vector3(10*i,0, 10 * k),Quaternion.identity);
                    ground.transform.parent = worldTile.transform;
                }
            }
        }
        StartPoint(randomGrid.roadList[1], randomGrid.roadList[0]);
        for(int i = 1; i < randomGrid.roadList.Count-1; i++)
        {
            nextPos = CheckNext(randomGrid.roadList[i + 1], randomGrid.roadList[i]);
            RoadAngle(randomGrid.roadList[i][0], randomGrid.roadList[i][1]);
        }
    }
    Dirrection CheckNext(Vector2Int nextPoz, Vector2Int currentPoz)
    {
        Dirrection alfa;
        if (nextPoz[0] - currentPoz[0] == 0) //Left or Right check
        {
            if(nextPoz[1]-currentPoz[1] == 1)
            {
                alfa = Dirrection.Right;
            }
            else
            {
                alfa = Dirrection.Left;
            }
        }
        else //Up or Down check
        {
            if(nextPoz[0] - currentPoz[0] == 1)
            {
                alfa = Dirrection.Down;
            }
            else
            {
                alfa = Dirrection.Up;
            }
        }
        return alfa;
    }
    void RoadAngle(int i , int k)
    {
        // 1-Up; 2-Right; 3-Down; 4-Left
        switch (lastPos)
        {
            case Dirrection.Up:
                switch (nextPos)
                {
                    case Dirrection.Right:
                        road = Instantiate(cornerRoadTile, new Vector3(i * 10, 0, k * 10), Quaternion.Euler(new Vector3(0, 270, 0)));
                        road.transform.parent = path.transform;
                        lastPos = Dirrection.Left;
                        break;
                    case Dirrection.Down:
                        road = Instantiate(straightRoadTile, new Vector3(i * 10, 0, k * 10), Quaternion.identity);
                        road.transform.parent = path.transform;
                        lastPos = Dirrection.Up;
                        break;
                    case Dirrection.Left:
                        road = Instantiate(cornerRoadTile, new Vector3(i * 10, 0, k * 10), Quaternion.Euler(new Vector3(0, 180, 0)));
                        road.transform.parent = path.transform;
                        lastPos = Dirrection.Right;
                        break;
                }
                break;
            case Dirrection.Right:
                switch (nextPos)
                {
                    case Dirrection.Down:
                        road = Instantiate(cornerRoadTile, new Vector3(i * 10, 0, k * 10), Quaternion.identity);
                        road.transform.parent = path.transform;
                        lastPos = Dirrection.Up;
                        break;
                    case Dirrection.Left:
                        road = Instantiate(straightRoadTile, new Vector3(i * 10, 0, k * 10), Quaternion.Euler(new Vector3(0,90,0)));
                        road.transform.parent = path.transform;
                        lastPos = Dirrection.Right;
                        break;
                    case Dirrection.Up:
                        road = Instantiate(cornerRoadTile, new Vector3(i * 10, 0, k * 10), Quaternion.Euler(new Vector3(0, 270, 0)));
                        road.transform.parent = path.transform;
                        lastPos = Dirrection.Down;
                        break;
                }
                break;
            case Dirrection.Down:
                switch (nextPos)
                {
                    case Dirrection.Left:
                        road = Instantiate(cornerRoadTile, new Vector3(i * 10, 0, k * 10), Quaternion.Euler(new Vector3(0, 90, 0)));
                        road.transform.parent = path.transform;
                        lastPos = Dirrection.Right;
                        break;
                    case Dirrection.Up:
                        road = Instantiate(straightRoadTile, new Vector3(i * 10, 0, k * 10), Quaternion.identity);
                        road.transform.parent = path.transform;
                        lastPos = Dirrection.Down;
                        break;
                    case Dirrection.Right:
                        road = Instantiate(cornerRoadTile, new Vector3(i * 10, 0, k * 10), Quaternion.identity);
                        road.transform.parent = path.transform;
                        lastPos = Dirrection.Left;
                        break;
                }
                break;
            case Dirrection.Left:
                switch (nextPos)
                {
                    case Dirrection.Up:
                        road = Instantiate(cornerRoadTile, new Vector3(i * 10, 0, k * 10), Quaternion.Euler(new Vector3(0, 180, 0)));
                        road.transform.parent = path.transform;
                        lastPos = Dirrection.Down;
                        break;
                    case Dirrection.Right:
                        road = Instantiate(straightRoadTile, new Vector3(i * 10, 0, k * 10), Quaternion.Euler(new Vector3(0, 90, 0)));
                        road.transform.parent = path.transform;
                        lastPos = Dirrection.Left;
                        break;
                    case Dirrection.Down:
                        road = Instantiate(cornerRoadTile, new Vector3(i * 10, 0, k * 10), Quaternion.Euler(new Vector3(0, 90, 0)));
                        road.transform.parent = path.transform;
                        lastPos = Dirrection.Up;
                        break;
                }
                break;
        }

    }
    void StartPoint(Vector2Int nextPoz, Vector2Int currentPoz)
    {
        if (nextPoz[0] - currentPoz[0] == 0) //Left or Right check
        {
            if (nextPoz[1] - currentPoz[1] == 1)
            {
                lastPos = Dirrection.Left;
            }
            else
            {
                lastPos = Dirrection.Right;
            }
        }
        else //Up or Down check
        {
            if (nextPoz[0] - currentPoz[0] == 1)
            {
                lastPos = Dirrection.Up;
            }
            else
            {
                lastPos = Dirrection.Down;
            }
        }
    }
}
