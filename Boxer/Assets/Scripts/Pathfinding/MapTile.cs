using UnityEngine;

/// <summary>
/// Represents a single tile. A tile can be passable or impassable.
/// </summary>
public class MapTile : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    public SpriteRenderer SpriteRenderer
    {
        get 
        {
            if (_spriteRenderer == null)
            {
                _spriteRenderer = GetComponent<SpriteRenderer>();
            }

            return _spriteRenderer;
        }
    }

    private bool _passable = true;
    public bool Passable
    {
        get => _passable;
        set
        {
            _passable = value;
            SetRendererColor();
        }
    }

    private void SetRendererColor()
    {
        SpriteRenderer.color = _passable ? Color.white : Color.black;
    }
}
