public class RobotController
{
    private enum State
    {
        Idle = 0,
        Lifting = 1,
        Throwing = 2,
        Thinking = 3
    }

    private State _state;

    private RobotComponent _component;
    public RobotController(RobotComponent component)
    {
        _component = component;
    }

    public void Tick()
    {
        if (_state == State.Idle)
        {
            Act();
        }
    }

    private void Act()
    {
        _state = State.Lifting;
        // TODO add behavior
    }
}
