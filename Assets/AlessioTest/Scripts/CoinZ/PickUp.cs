using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickUp : MonoBehaviour , IPickable
{
    [Header("PickUp Settings")]
    [SerializeField] private float _altitude = 0.1f;
    [SerializeField] private float _rotationSpeed = 150f;
    [SerializeField] private float _altitudeSpeed = 1.0f;
    [SerializeField] private float _respawnDelay = 5f;

    [SerializeField] private GameObject _graphicObject;

    [Header("Events")]
    [SerializeField] private UnityEvent _onPick;

    private Vector3 _position;

    private void Start()
    {
        _position = transform.position;
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);
        float offsetY = Mathf.Sin(Time.time * _altitudeSpeed) * _altitude;
        transform.position = _position + Vector3.up * offsetY;
    }

    public void Pick()
    {
        OnPick(PlayerManager.Instance.CurrentPlayer.gameObject);
    }

    protected virtual void OnPick(GameObject player)
    {
        _onPick.Invoke();
        StartCoroutine(RespawnCoroutine());
    }

    private IEnumerator RespawnCoroutine()
    {
        _graphicObject.SetActive(false);

        yield return new WaitForSeconds(_respawnDelay);

        _graphicObject.SetActive(true);
    }
}
