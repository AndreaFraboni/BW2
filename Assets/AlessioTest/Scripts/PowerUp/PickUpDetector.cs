using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpDetector : MonoBehaviour
{
    [SerializeField] private float _pickUpRadius = 2f;
    [SerializeField] private LayerMask _pickupLayer;

    private float _magnetRadius;
    private bool _magnetActive = false;

    private void Update()
    {
        float radius = _magnetActive ? _magnetRadius : _pickUpRadius;
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, _pickupLayer);

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out IPickable pickable))
                pickable.Pick();
        }
    }

    public void ActivateMagnet(float duration)
    {
        StartCoroutine(MagnetCoroutine(duration));
    }
    private IEnumerator MagnetCoroutine(float duration)
    {
        _magnetActive = true;
        _magnetRadius = _pickUpRadius * 5f;
        yield return new WaitForSeconds(duration);
        _magnetActive = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _pickUpRadius);
    }
}
