using UnityEngine;

/// <summary>
/// "Physical" representation of the game's AI. Used to start the game and tick time for AI controllers.
/// </summary>
public class DirectorComponent : MonoBehaviour
{
   public delegate void TickHandler();
   public event TickHandler Tick;
   
   private LevelComponent _level;
   public LevelComponent Level
   {
      get
      {
         if (_level == null)
         {
            _level = FindObjectOfType<LevelComponent>(); // TODO replace with a load
         }

         return _level;
      }
   }

   public void Awake()
   {
      DirectorController controller = new DirectorController();
      controller.Initialize(this);
   }

   public void Update()
   {
      Tick?.Invoke();
   }
}
