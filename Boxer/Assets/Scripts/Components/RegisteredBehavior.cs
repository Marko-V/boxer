using System;
using UnityEngine;

/// <summary>
/// A behavior whose existence is tracked. Used to update references to the object as it gets destroyed.
/// </summary>
public class RegisteredBehavior : MonoBehaviour
{
    public event Action<RegisteredBehavior> OnDestroyed;

    public void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
    }
}
