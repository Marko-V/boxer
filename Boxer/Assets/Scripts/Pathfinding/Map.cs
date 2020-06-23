using UnityEngine;

/// <summary>
/// The root class for this demo.
/// </summary>
public class Map : MonoBehaviour
{
    public MapGenerator Generator;
    public MapInteractor Interactor;
    public MapPainter Painter;
    public MapPathfinder Pathfinder;

    public MapGrid Grid { get; private set; }

    private void Start()
    {
        Grid = Generator.GenerateMap();
        Painter.Initialize(this);
        Pathfinder.Initialize(this);
        Interactor.Initialize(this);
    }
}
