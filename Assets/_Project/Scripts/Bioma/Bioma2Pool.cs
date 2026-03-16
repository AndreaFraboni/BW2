using System.Collections.Generic;
using UnityEngine;

public class Bioma2Pool : MonoBehaviour
{
    public static Bioma2Pool Instance { get; private set; }

    [SerializeField] private GameObject[] tilePrefabs;
    [SerializeField] private int poolSize = 20;

    private Queue<GameObject> pool = new Queue<GameObject>();

    public GameObject finalTilePrefab;

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

    public void HideAllPoolObject()
    {
        foreach (GameObject obj in pool)
        {
            if (obj != null) obj.SetActive(false);
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