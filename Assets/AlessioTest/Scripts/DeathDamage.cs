using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathDamage : MonoBehaviour
{
    [SerializeField] private GameObject _graphicObject;
    [SerializeField] private int _damageAmount = 1;
   
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<LifeController>(out var lifeController)) return;
        lifeController.TakeDamage(_damageAmount);
        StartCoroutine(DisableCoroutine());
    }

    private IEnumerator DisableCoroutine()
    {
        _graphicObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        _graphicObject.SetActive(true);

    }
}
