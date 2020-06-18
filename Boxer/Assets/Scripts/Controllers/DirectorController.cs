public class DirectorController
{
    private DirectorComponent _component;

    private LevelController _level;

    public void Initialize(DirectorComponent component)
    {
        _component = component;
        _component.Tick += Tick;

        InitializeControls();
        Run();
    }

    private void InitializeControls()
    {
        _level = new LevelController(_component.Level);
        _level.Populate();
    }

    private void Run()
    {
    }

    private void Tick()
    {
        TickControls();
    }

    private void TickControls()
    {
        _level.Robot.Tick();
    }
}
