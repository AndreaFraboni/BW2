using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private float _groundDistance;
    [SerializeField] private LayerMask _ground;

    public bool IsGrounded;

    private void Update()
    {
        IsGrounded = CanJump();
    }

    public bool CanJump()
    {
        return (Physics.CheckSphere(transform.position, _groundDistance, _ground, QueryTriggerInteraction.Ignore));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _groundDistance);
    }
}
