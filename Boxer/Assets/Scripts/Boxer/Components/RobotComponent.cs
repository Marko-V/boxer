using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the robot in the game world.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(CollisionComponent))]
public class RobotComponent : MonoBehaviour
{
    public enum HorizontalDirection
    {
        Stationary,
        Left,
        Right
    }

    public HorizontalDirection MovementDirection = HorizontalDirection.Stationary;
    public float MovementForcePerSecond = 600;
    public float JumpForce = 10;

    private Rigidbody2D _rigidbody;
    public CollisionComponent Collision => GetComponent<CollisionComponent>();

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        ProcessMovement();
    }

    private void ProcessMovement()
    {
        // get direction
        int directionMultiplier = 0;
        switch (MovementDirection)
        {
            case HorizontalDirection.Left:
                directionMultiplier = -1;
                break;
            case HorizontalDirection.Right:
                directionMultiplier = 1;
                break;
            case HorizontalDirection.Stationary:
                directionMultiplier = 0;
                break;
        }
        
        // apply force to move
        _rigidbody.AddForce(directionMultiplier * Time.deltaTime * new Vector2(MovementForcePerSecond, 0));
    }

    public void Jump()
    {
        _rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
    }

    public bool IsStandingOnSolidGround()
    {
        List<RaycastHit2D> results = new List<RaycastHit2D>();
        _rigidbody.Cast(Vector2.down, results, 0.05f);
        return results.Count > 0; // TODO better filtering
    }
}
