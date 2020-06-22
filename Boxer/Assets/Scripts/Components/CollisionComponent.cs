using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable] 
public class CollisionEvent : UnityEvent<Collision2D> { }

[RequireComponent(typeof(Collider2D))]
public class CollisionComponent : MonoBehaviour
{
    public CollisionEvent OnCollisionDetected;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        OnCollisionDetected?.Invoke(other);
    }
}
