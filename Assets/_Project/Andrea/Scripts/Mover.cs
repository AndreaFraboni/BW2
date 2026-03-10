using UnityEngine;

public class Mover : MonoBehaviour
{
    private Rigidbody _rb;
    private PlayerController _pc;
    private float _speed;

    private Vector3 currentDirection;

    private void Awake()
    {
        if (_rb == null) _rb = GetComponentInParent<Rigidbody>();
        if (_pc == null) _pc = GetComponentInParent<PlayerController>();
    }

    private void FixedUpdate()
    {
        if (!_pc.isAlive) return;
        if (_pc.isFiring)
        {
            _rb.velocity = new Vector3(0f, _rb.velocity.y, 0f);
            return;
        }

        if (currentDirection.magnitude > 0.01f)
        {
            Vector3 velocity = currentDirection * _speed;
            if (!_pc.isFiring) _rb.velocity = new Vector3(velocity.x, _rb.velocity.y, velocity.z);
        }
        else
        {
            _rb.velocity = new Vector3(0f, _rb.velocity.y, 0f); // azzero per non avere l'inerzia nel movimento finale dopo aver smesso di premere i tasti di movimento cosi si ferma subito
        }
    }

    public void SetInput(Vector3 input)
    {
        currentDirection = input;
    }

    public void SetAndNormalizeInput(Vector3 input)
    {
        input.y = 0f;
        if (input.sqrMagnitude > 1f) input.Normalize();
        SetInput(input);
    }
    public void SetSpeed(float speed)
    {
        _speed = speed;
    }
}

