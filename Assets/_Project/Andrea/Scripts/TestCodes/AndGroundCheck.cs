using UnityEngine;
public class AndGroundCheck : MonoBehaviour
{
    [SerializeField] private float _probeDistance = 0.1f;
    [SerializeField] private LayerMask _layerGroundMask;
    [SerializeField] private AndController _playerController;

    private void Awake()
    {
        if (_playerController == null) _playerController = GetComponentInParent<AndController>();
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
