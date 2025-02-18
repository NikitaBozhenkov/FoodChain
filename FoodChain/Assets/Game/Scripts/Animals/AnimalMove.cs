using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class AnimalMove : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody _rigidbody;
    private Vector2 _moveDirection;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        transform.forward = CalculateRandomDirection();
    }

    private void Update()
    {
        _rigidbody.velocity = transform.forward * _speed;
    }

    private Vector3 CalculateRandomDirection()
    {
        var randomPoint = Random.insideUnitCircle.normalized;
        return new Vector3(randomPoint.x, 0, randomPoint.y);
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            transform.Rotate(Vector3.up, 180);
            transform.forward = CalculateRandomDirection();
        }
    }
}
