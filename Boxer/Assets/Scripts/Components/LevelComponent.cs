using System.Collections.Generic;
using UnityEngine;

public class LevelComponent : MonoBehaviour
{
    [Header("Boxes")]
    public BoxCollider2D BoxSpawnArea; // does not account for rotation
    public int BoxesToSpawn = 8;
    public BoxComponent Box;
    public List<BoxComponent> InstantiatedBoxes = new List<BoxComponent>();
    
    [Header("Containers")]
    public ContainerComponent Container;
    public Transform[] ContainerSpawns; 
    public List<ContainerComponent> InstantiatedContainers = new List<ContainerComponent>();
    
    [Header("Robot")]
    public RobotComponent Robot;
    public Transform RobotSpawn;
    public List<RobotComponent> InstantiatedRobots = new List<RobotComponent>();

    public BoxComponent CreateBox(Vector2 position)
    {
        BoxComponent newBox = Instantiate(Box, position, Quaternion.identity, transform);
        newBox.gameObject.SetActive(true);
        InstantiatedBoxes.Add(newBox);
        return newBox;
    }

    public ContainerComponent CreateContainer(Vector3 position)
    {
        ContainerComponent newContainer = Instantiate(Container, position, Quaternion.identity, transform);
        newContainer.gameObject.SetActive(true);
        InstantiatedContainers.Add(newContainer);
        return newContainer;
    }

    public RobotComponent CreateRobot()
    {
        RobotComponent newRobot = Instantiate(Robot, RobotSpawn.position, Quaternion.identity, transform);
        newRobot.gameObject.SetActive(true);
        InstantiatedRobots.Add(newRobot);
        return newRobot;
    }
}
