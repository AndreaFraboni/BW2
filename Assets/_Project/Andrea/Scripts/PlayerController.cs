using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Player movement parameters")]
    [SerializeField] private float _speed = 6.0f;
    [SerializeField] private float _smooth = 10f;
    [SerializeField] private float _jumpForce = 5f;

    private Mover _mover;
    private Rotator _rotator;
  
    private Rigidbody _rb;
    
    private CapsuleCollider _capsuleCollider;

    private float horizontal, vertical = 0f;
    private Vector3 currentDirection = Vector3.zero;

    private Camera _cam;
    private Ray _ray;

    public bool isAlive = true;
    public bool isGrounded = false;
    public bool isJump = false;
    public bool isRunning = false;
    public bool isFiring = false;
    public bool isFalling = false;

    public int _currentCoins = 0;

    // Getter
    public Vector3 GetDirection() => currentDirection;

    private void Awake()
    {
        if (_rb == null) _rb = GetComponent<Rigidbody>();
        if (_mover == null) _mover = GetComponent<Mover>();
        if (_rotator == null) _rotator = GetComponent<Rotator>();
        if (_capsuleCollider == null) _capsuleCollider = GetComponent<CapsuleCollider>();
        _cam = Camera.main;
    }

    void Update()
    {
        CheckInput();
        CheckRun();
        CheckJump();
    }

    private void FixedUpdate()
    {
        Move();
        Rotation();

        if (isJump) Jump();
    }

    private void CheckInput()
    {
        if (!isAlive) return;
        if (isFiring) return;

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        Vector3 targetDirection = Vector3.zero;
        targetDirection = _cam.transform.forward * vertical + _cam.transform.right * horizontal;
        targetDirection.y = 0f;

        if (targetDirection.magnitude > 0.01f) targetDirection.Normalize();

        currentDirection = Vector3.Lerp(currentDirection, targetDirection, _smooth * Time.deltaTime);
    }

    private void CheckRun()
    {
        if (!isAlive) return;
        if (isFiring) return;

        isRunning = (Input.GetKey("left shift"));
    }

    private void CheckJump()
    {
        if (!isAlive) return;
        if (isFiring) return;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJump = true;
            isGrounded = false;
        }
    }

    private void Move()
    {
        if (!isAlive) return;
        if (isFiring) return;

        if (_mover != null && !isFiring)
        {
            if (isRunning)
            {
                _mover.SetSpeed(_speed * 2);
                _mover.SetAndNormalizeInput(currentDirection);
            }
            else
            {
                _mover.SetSpeed(_speed);
                _mover.SetAndNormalizeInput(currentDirection);
            }
        }
    }

    private void Rotation()
    {
        if (!isAlive) return;
        if (isFiring) return;

        if (currentDirection.sqrMagnitude < 0.0004f) return;

        if (_rotator != null) _rotator.SetRotation(currentDirection);
    }

    private void Jump()
    {
        if (!isAlive) return;
        if (isFiring) return;

        isJump = false;
        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    public void DestroyGOPlayer()
    {
        Destroy(gameObject);
    }

}

