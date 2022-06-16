using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

public class MapLoad : MonoBehaviour
{
    string path;
    public MapCreate mapCreate;

    public void OpenFileExplorer()
    {
        path = EditorUtility.OpenFilePanel("Load txt", "", "dat,txt");
    }
    public void SaveFileExplorer()
    {
        if (mapCreate.ReturnMap() != null)
        {
            string local = EditorUtility.SaveFilePanel("Save Window", "", "", "dat");
            SaveFile(local);
        }
        else
        {
            Debug.Log("No map to save");
        }
    }
    public void SaveFile(string path)
    {
        string destination = path;
        FileStream file;

        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);

        MapData data = mapCreate.ReturnMapData();
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }
    public void LoadFile()
    {
        OpenFileExplorer();
        string destination = path;
        FileStream file;

        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            Debug.LogError("File not found");
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        MapData data = (MapData)bf.Deserialize(file);
        file.Close();

        mapCreate.sizeA = data.GetLenght();
        mapCreate.sizeB = data.GetWidth();
        mapCreate.SetMap(data.GetGridMap());

        Debug.Log(data.GetLenght());
        Debug.Log(data.GetWidth());
        mapCreate.randomGrid.PrintArray(data.GetGridMap());
        mapCreate.GenMap(data.RestoreRoadVector(),data.GetGridMap(),data.GetLenght(),data.GetWidth());
    }
    
}
