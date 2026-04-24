using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    private GameObject DetectObject(Camera mainCamera)
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider != null)
            {
                return (hit.collider.gameObject);
            }
        }
        return null;
    }
    static public Vector3 GetMouseWorldPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }

    static public void DispalyList<T>(List<T> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            Debug.Log("Element " +i+": " + list[i].ToString());
        }
    }
    static public void DispalyQueue<T>(Queue<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Debug.Log("Element " + i + ": " + list.Dequeue().ToString());
        }
    }
    static public void DispalyArray<T>(T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            Debug.Log("Element " + i + ": " + array[i].ToString());
        }
    }
    static public void DisplayDictionary<K,T>(Dictionary<K,T> dictionary)
    {
        foreach(KeyValuePair<K, T> keyStat in dictionary)
        {
            Debug.Log(keyStat.Key);
            Debug.Log(keyStat.Value);
        }
    }


    //This function gets a cell object of generic type and applies a function using it as a parameter
    //The parameters are the method to be called and the grid
    public static void CellToWaypoint<T>(System.Action<T> callMethod , GridMapScript<T> gridMap)
    {

        gridMap.GetCell(Utils.GetMouseWorldPoint(), out int x, out int z);

        if (gridMap.CheckIfInGrid(x, z))
        {
            var way = gridMap.GetCellValue(x, z);
            callMethod(way);
        }
    }
}
