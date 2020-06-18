using UnityEngine;
public enum ColorName
{
    Colorless = 0,
    Red = 1,
    Blue = 2
}

public class ColorComponent : MonoBehaviour
{
    public ColorName Color;

    private void Start()
    {
        AssignColor();
    }

    private void AssignColor()
    {
        if (Color == ColorName.Colorless)
        {
            Color = (ColorName) Random.Range(1, 3);
        }

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = GetColorForName(Color);
        }
    }

    private Color GetColorForName(ColorName colorName)
    {
        Color color;
        switch (Color)
        {
            case ColorName.Blue:
                color = UnityEngine.Color.blue;
                break;
            case ColorName.Red:
                color = UnityEngine.Color.red;
                break;
            default:
                color = UnityEngine.Color.white;
                break;
        }

        return color;
    }
}
