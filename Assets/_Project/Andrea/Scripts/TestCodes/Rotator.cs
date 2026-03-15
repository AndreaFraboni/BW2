using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 5f;

    private Rigidbody _rb;
    private Vector3 _rotmove;

    private void Awake()
    {
        _rb = GetComponentInParent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Rotation();
    }

    public void SetRotation(Vector3 input)
    {
        _rotmove = input;
    }

    public void Rotation()
    {
        if (_rotmove != Vector3.zero)
        {
            _rotmove.y = 0f; // bad orientation of player fixed by this row
            if (_rotmove.sqrMagnitude < 0.0001f) return;

            Quaternion _rotation = Quaternion.LookRotation(_rotmove, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, _rotation, Time.fixedDeltaTime * _rotationSpeed);
        }
    }

}
