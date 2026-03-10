using UnityEngine;
public class GroundCheck : MonoBehaviour
{
    [SerializeField] private float _probeDistance = 0.1f;
    [SerializeField] private LayerMask _layerGroundMask;
    [SerializeField] private PlayerController _playerController;

    private void Awake()
    {
        if (_playerController == null) _playerController = GetComponentInParent<PlayerController>();
        if (_layerGroundMask == 0) _layerGroundMask = LayerMask.GetMask("Ground");
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void CheckGround()
    {
        bool grounded = Physics.Raycast(transform.position, Vector3.down, _probeDistance, _layerGroundMask);
        _playerController.isGrounded = grounded;
    }
}
