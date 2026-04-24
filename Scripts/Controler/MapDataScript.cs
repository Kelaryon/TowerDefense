using UnityEngine;

[CreateAssetMenu]
public class MapDataScript : ScriptableObject
{
    [SerializeField] int sizeX;
    [SerializeField] int sizeY;
    [SerializeField] int startBallance;
    readonly float cellSize = 10;

    public int SizeX { get => sizeX; }
    public int SizeY { get => sizeY; }
    public int StartBallance { get => startBallance; }
    public float CellSize => cellSize;
}
