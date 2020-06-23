using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles player input.
/// </summary>
public class MapInteractor : MonoBehaviour
{
    private Map _map;
    private Camera _camera;
    private bool _initialized;

    private List<Vector3> _previousMouseClicks = new List<Vector3>();
    
    public void Initialize(Map map)
    {
        _map = map;
        _camera = Camera.main;
        _initialized = true;
    }

    private void Update()
    {
        if (!_initialized)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseClickWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            _previousMouseClicks.Add(mouseClickWorldPosition);
            if (_previousMouseClicks.Count > 1)
            {
                int count = _previousMouseClicks.Count;
                _map.Pathfinder.CalculateAndMarkPath(_previousMouseClicks[count - 1], _previousMouseClicks[count - 2]);
            }
        }
        if (Input.GetMouseButton(1))
        {
            Vector2 mouseClickWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            _map.Painter.TurnImpassable(mouseClickWorldPosition);
        }

        if (Input.GetKeyDown(KeyCode.Plus))
        {
            _map.Painter.BrushSize = Mathf.Min(10, _map.Painter.BrushSize + 1);
        } 
        else if (Input.GetKeyDown(KeyCode.Minus))
        {
            _map.Painter.BrushSize = Mathf.Max(1, _map.Painter.BrushSize - 1);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.R))
        {
            _map.Painter.ResetLevel();
        } 
        else if (Input.GetKeyDown(KeyCode.I))
        {
            _map.Painter.InvertLevel();
        }

    }
}
