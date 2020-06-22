using UnityEngine;

/// <summary>
/// The monobehaviour representing the containers that boxes go into.
/// </summary>
[RequireComponent(typeof(ColorComponent))]
public class ContainerComponent : MonoBehaviour
{
    public ColorName Color
    {
        get => ColorComponent.Color;
        set => ColorComponent.Color = value;
    }

    private ColorComponent _colorComponent;
    public ColorComponent ColorComponent
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

    public void BottomCollisionDetected(Collision2D other)
    {
        BoxComponent box = other.gameObject.GetComponent<BoxComponent>();
        if (box != null)
        {
            if (box.Color == Color)
            {
                Destroy(box.gameObject);
            }
            else
            {
                Rigidbody2D boxRigidbody = box.GetComponent<Rigidbody2D>();
                boxRigidbody.AddForce(Vector2.up * 10);
            }
        }
    }
}
