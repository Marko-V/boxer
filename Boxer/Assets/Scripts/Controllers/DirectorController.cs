using UnityEngine;

/// <summary>
/// Runs and oversees the world.
/// </summary>
public class DirectorController
{
    private DirectorComponent _component;

    private LevelController _level;
    private SpawnController _spawn;

    public void Initialize(DirectorComponent component)
    {
        _component = component;
        _component.Tick += Tick;

        InitializeControls();
        Run();
    }

    ~DirectorController()
    {
        if (_component != null)
        {
            _component.Tick -= Tick;
        }
    }

    private void InitializeControls()
    {
        _level = new LevelController(_component.Level);
        _spawn = new SpawnController(_level, _component.Level);
    }

    private void Run()
    {
        _spawn.Populate();
        _level.Robot.MakeAware(_level);
    }

    private void Tick()
    {
        TickControls();
        if (Input.GetKeyDown(KeyCode.Space)) // TODO input controller
        {
            _level.Robot.Jump();
        }
    }

    private void TickControls()
    {
        _level.Robot.Tick();
    }
}
