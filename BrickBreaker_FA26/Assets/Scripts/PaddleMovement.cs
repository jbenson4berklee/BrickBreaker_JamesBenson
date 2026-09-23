using System;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{

    [SerializeField] private float _speed = 6.0f;

    private float _direction = 0.0f;
    
    [SerializeField] private KeyCode LeftDirection = KeyCode.LeftArrow;
    [SerializeField] private KeyCode RightDirection = KeyCode.RightArrow;

    private Rigidbody2D _rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rb.linearVelocityX = _direction * _speed;
    }

    // Update is called once per frame
    void Update()
    {
        _direction = 0.0f;
        
        if (Input.GetKey(LeftDirection))
        {
            _direction -= 1.0f;
        }

        if (Input.GetKey(RightDirection))
        {
            _direction += 1.0f;
        }
    }
}
