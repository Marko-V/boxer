using UnityEngine;

/// <summary>
/// The monobehaviour representing a box.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(ColorComponent))]
public class BoxComponent : RegisteredBehavior
{
    public ColorName Color
    {
        get => ColorComponent.Color;
        set => ColorComponent.Color = value;
    }

    private ColorComponent _colorComponent;

    protected ColorComponent ColorComponent
    {
        get
        {
            if (_colorComponent == null)
            {
                _colorComponent = GetComponent<ColorComponent>();
            }

            return _colorComponent;
        }
    }

    private Rigidbody2D _rigidbody;

    public Rigidbody2D RigidBody
    {
        get
        {
            if (_rigidbody == null)
            {
                _rigidbody = GetComponent<Rigidbody2D>();
            }

            return _rigidbody;
        }
    }

    private Transform _transform;

    public Transform Transform
    {
        get
        {
            if (_transform == null)
            {
                _transform = transform;
            }

            return _transform;
        }
    }

}
