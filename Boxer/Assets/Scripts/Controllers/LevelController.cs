using UnityEngine;

/// <summary>
/// Used to hold information about the level and process it as necessary.
/// </summary>
public class LevelController
{
   private LevelComponent _component;

   public RobotController Robot;
   
   public LevelController(LevelComponent component)
   {
      _component = component;
   }

   public ContainerComponent GetContainer(ColorName color)
   {
      ContainerComponent foundContainer = null;
      foreach (ContainerComponent container in _component.InstantiatedContainers)
      {
         if (container.Color == color)
         {
            foundContainer = container;
         }
      }

      return foundContainer;
   }

   public BoxComponent GetClosestBox(Vector2 position, float preferredHeightDifference = 1.0f)
   {
      BoxComponent foundBox = null;
      float distance = int.MaxValue;

      if (_component.InstantiatedBoxes.Count > 0)
      {
         foreach (BoxComponent box in _component.InstantiatedBoxes)
         {
            if (Mathf.Abs(box.transform.position.y - position.y) < preferredHeightDifference)
            {
               float newDistance = Vector2.Distance(box.transform.position, position);
               if (distance > newDistance)
               {
                  distance = newDistance;
                  foundBox = box;
               }
            }
         }

         if (foundBox == null)
         {
            GetClosestBox(position, float.MaxValue);
         }
      }

      return foundBox;
   }
}
