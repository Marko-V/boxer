using System;
using UnityEngine;

/// <summary>
/// Used to spawn level objects and register them with the level.
/// </summary>
public class SpawnController
{
    private LevelController _level;
    private LevelComponent _component;

    private bool _shouldSpawn = false;
    public SpawnController(LevelController levelController, LevelComponent component)
    {
        _level = levelController;
        _component = component;
    }
    
    public void Populate()
    {
        SpawnBoxes();
        SpawnContainers();
        SpawnRobot();
    }

    public void SpawnMoreBoxesIfNecessary()
    {
        if (_shouldSpawn)
        {
            SpawnBoxes();
        }
    }

    private void SpawnBoxes()
    {
        for (int i = 0; i < _component.BoxesToSpawn; i++)
        {
            Vector2 point = MathUtils.GetRandomPointWithinBounds(_component.BoxSpawnArea.bounds);
            BoxComponent box = _component.CreateBox(point);
            box.OnDestroyed += BoxDestroyed;
        }

        _shouldSpawn = false;
    }

    private void BoxDestroyed(RegisteredBehavior boxComponent)
    {
        if (boxComponent is BoxComponent box)
        {
            _component.InstantiatedBoxes.Remove(box);
            if (_component.InstantiatedBoxes.Count == 0)
            {
                // instantiation should not be done through OnDestroy, so we're delaying it
                // unfortunately this means we use polling (update), rather than events
                _shouldSpawn = true;
            }
        }
    }

    private void SpawnContainers()
    {
        if (_component.ContainerSpawns.Length < 2)
        {
            Debug.LogWarning("Warning: insufficient containers. Link more spawn points to containers.");
        }

        ColorName colorName = ColorName.Colorless;
        foreach(Transform spawn in _component.ContainerSpawns)
        {
            if (Enum.IsDefined(typeof(ColorName), ++colorName))
            {
                ContainerComponent containerComponent = _component.CreateContainer(spawn.position);
                containerComponent.Color = colorName;
            }
            else
            {
                break;
            }
        }
    }

    private void SpawnRobot()
    {
        RobotComponent robotComponent = _component.CreateRobot();
        _level.Robot = new RobotController(robotComponent);
    }
}
