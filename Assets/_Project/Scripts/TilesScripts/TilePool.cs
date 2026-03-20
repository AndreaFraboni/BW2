using System.Collections.Generic;
using UnityEngine;

public class TilePool : MonoBehaviour
{
    public static TilePool Instance { get; private set; }

    [SerializeField] private GameObject[] tilePrefabs;
    [SerializeField] private int poolSize = 10;

    private Queue<GameObject> pool = new Queue<GameObject>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        CreatePool(poolSize);
    }

    public void CreatePool(int num)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject prefab = tilePrefabs[i % tilePrefabs.Length];
            GameObject obj = Instantiate(prefab, transform);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject GetPoolObj()
    {
        if (pool.Count == 0)
            CreatePool(1);

        return pool.Dequeue();
    }

    public void PutPoolObj(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}