using UnityEngine;

/// <summary>
/// Controls robot behavior through the RobotComponent.
/// </summary>
public class RobotController
{
    private enum State
    {
        Idle = 0,
        Moving = 1,
        Throwing = 2
    }

    private State _state;

    private LevelController _level;
    private RobotComponent _component;
    public RobotController(RobotComponent component)
    {
        _component = component;
        _component.Collision.OnCollisionDetected.AddListener(OnCollisionEnter);
    }

    public void MakeAware(LevelController levelController)
    {
        _level = levelController;
    }

    public void Tick()
    {
        if (_state == State.Idle || _state == State.Moving) // go find a box
        {
            FindBoxAndMoveTowardIt();
        }
    }

    public void Jump()
    {
        if (_state == State.Moving && _component.IsStandingOnSolidGround())
        {
            _component.Jump();
        }
    }

    private void FindBoxAndMoveTowardIt()
    {
        BoxComponent box = _level.GetClosestBox(_component.transform.position);
        if (box != null)
        {
            _state = State.Moving;
            if (box.Transform.position.x - _component.transform.position.x > 0) // box is on the right
            {
                _component.MovementDirection = RobotComponent.HorizontalDirection.Right;
            }
            else // box is on the left
            {
                _component.MovementDirection = RobotComponent.HorizontalDirection.Left;
            }
        }
    }

    private void OnCollisionEnter(Collision2D other)
    {
        BoxComponent boxComponent = other.gameObject.GetComponent<BoxComponent>();
        if (boxComponent != null)
        {
            _component.MovementDirection = RobotComponent.HorizontalDirection.Stationary;
            _state = State.Throwing;
            boxComponent.Transform.position = _component.transform.position + Vector3.up; // TODO animate
            ContainerComponent container = _level.GetContainer(boxComponent.Color);
            float throwDistance = container.transform.position.x - _component.transform.position.x; // TODO make better estimate
            float throwDirection = Mathf.Sign(throwDistance);
            float throwForce = Mathf.Abs(throwDistance);
            boxComponent.RigidBody.AddForce(throwForce * new Vector2(throwDirection, 1), ForceMode2D.Impulse);
            _state = State.Idle;
        }
    }
}
