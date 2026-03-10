using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _addGravity;
    [SerializeField] private float _dashForce = 10f;
    [SerializeField] private float _dashDuration = 0.2f;

    private Rigidbody _rb;
    private GroundCheck _gc;
    private bool _isDashing = false;
    private float _dashTime = 0f;
    private float _direction;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _gc = GetComponentInChildren<GroundCheck>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && _gc.IsGrounded)
            Jump();

        if (!_isDashing)
        {
            if (Input.GetKeyDown(KeyCode.A))
                Dash(-1);

            if (Input.GetKeyDown(KeyCode.D))
                Dash(1);
        }
    }

    private void FixedUpdate()
    {
        Vector3 horizontal = new Vector3(transform.forward.x * _speed, 0, transform.forward.z * _speed);

        if (_isDashing)
        {
            _dashTime += Time.fixedDeltaTime;

            horizontal.x = _dashForce * _direction;

            if (_dashTime >= _dashDuration)
            {
                _isDashing = false; 
            }
        }

        _rb.velocity = new Vector3(horizontal.x, _rb.velocity.y, horizontal.z);

        if (_rb.velocity.y < 0)
        {
            _rb.AddForce(Vector3.down * _addGravity);
        }
    }

    private void Jump()
    {
        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    private void Dash(int direction)
    {
        _isDashing = true;
        _direction = direction;
        _dashTime = 0f;
    }
}

