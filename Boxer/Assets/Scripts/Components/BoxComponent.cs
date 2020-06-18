using UnityEngine;

[RequireComponent(typeof(ColorComponent))]
public class BoxComponent : MonoBehaviour
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
}
