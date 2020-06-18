using System.ComponentModel;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolygonCollider2D))]
public class RobotComponent : MonoBehaviour
{
    /* TODO:
     * Robot components:
     * - movement and actions
     * - collisions and rendering
     */

    private bool _moving = true;
    private SpriteRenderer _renderer;
    private PolygonCollider2D _collider;
    private Rigidbody2D _rigidbody;

    private float _xMovementValue;
    private float _lastTimeChangedDirection;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<PolygonCollider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _xMovementValue = Random.value > 0.5f ? -1 : 1;
    }
    
    private void Update() // TODO move to controller
    {
        if (_moving)
        {
            _rigidbody.AddForce(600 * Time.deltaTime * new Vector2(_xMovementValue, 0));
            if (Time.time - _lastTimeChangedDirection > 2.5f)
            {
                _xMovementValue = -_xMovementValue;
                _lastTimeChangedDirection = Time.time;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        BoxComponent boxComponent = other.gameObject.GetComponent<BoxComponent>();
        if (boxComponent != null)
        {
            _moving = false;
            Rigidbody2D otherRigidbody = other.gameObject.GetComponent<Rigidbody2D>();
            if (otherRigidbody == null)
            {
                otherRigidbody = other.gameObject.AddComponent<Rigidbody2D>();
            }

            otherRigidbody.AddForce(10 * new Vector2(boxComponent.Color == ColorName.Blue ? 1 : -1, 1), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<BoxComponent>() && !_moving)
        {
            _moving = true;
        }
    }
}
