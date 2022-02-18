using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 1f;
    
    private Vector3 _forward = Vector3.right;
    private Vector3 _left = Vector3.forward;
    
    private enum Direction
    {
        Forward,
        Left
    }

    private Vector3 GetDirection()
    {
        if (_currentDirection == Direction.Forward)
            return _forward;
        return _left;
    }

    private Direction _currentDirection = Direction.Forward;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_currentDirection == Direction.Forward)
                _currentDirection = Direction.Left;
            else
                _currentDirection = Direction.Forward;
        }
    }

    private void FixedUpdate()
    {
        Vector3 velocity = GetDirection() * Speed;
        velocity.y = _rigidbody.velocity.y;
        _rigidbody.velocity = velocity;
    }

    private void OnDisable()
    {
        Vector3 velocity = Vector3.zero;
        velocity.y = _rigidbody.velocity.y;
        _rigidbody.velocity = velocity;
    }
}
