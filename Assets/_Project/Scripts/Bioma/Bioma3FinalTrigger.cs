using UnityEngine;

public class Bioma3FinalTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TileSpawner.Instance.restartBioma1();
        }
    }


}
