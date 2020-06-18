using System;
using UnityEngine;

public class LevelController
{
   private LevelComponent _component;

   public RobotController Robot;
   
   public LevelController(LevelComponent component)
   {
      _component = component;
   }

   public void Populate()
   {
      SpawnBoxes();
      SpawnContainers();
      SpawnRobot();
   }

   private void SpawnBoxes()
   {
      for (int i = 0; i < _component.BoxesToSpawn; i++)
      {
         Vector2 point = MathUtils.GetRandomPointWithinBounds(_component.BoxSpawnArea.bounds);
         _component.CreateBox(point);
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
      Robot = new RobotController(robotComponent);
   }
}
