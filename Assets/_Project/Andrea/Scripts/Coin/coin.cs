using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("COIN object 3D Parameters")]
    [SerializeField] private float _coinRotSpeed = 100f;
    //[SerializeField] private int _coinValue = 10;

    void Update()
    {
        transform.Rotate(_coinRotSpeed * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag(Tags.Player))
        //{
        //    other.gameObject.GetComponent<PlayerController>().GetCoins(_coinValue);
        //    Destroy(gameObject);
        //}
    }
}
