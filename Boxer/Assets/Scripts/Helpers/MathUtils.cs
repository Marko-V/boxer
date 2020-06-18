using UnityEngine;

public static class MathUtils
{
    public static Vector2 GetRandomPointWithinBounds(Bounds bounds)
    {
        return new Vector2(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y));
    }
}
