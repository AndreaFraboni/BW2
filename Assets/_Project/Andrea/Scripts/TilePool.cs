using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePool : MonoBehaviour
{
    public static TilePool _instance;

    [SerializeField] private GameObject _prefabTile;
    [SerializeField] private int _poolSize;

    Queue<GameObject> pool = new Queue<GameObject>();

    public static TilePool Instance
    {
        get
        {
            // Se non esiste già un'istanza, cerca nel gioco
            if (_instance == null)
            {
                _instance = FindObjectOfType<TilePool>();

                // Se non è stato trovato, crea un nuovo oggetto Tile Pool
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("TilePool");
                    _instance = singletonObject.AddComponent<TilePool>();
                    singletonObject.tag = "TilePool";
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CreatePool(_poolSize);
    }
        public void CreatePool(int num)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject obj = Instantiate(_prefabTile, transform);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject GetPoolObj()
    {
        if (pool.Count == 0)
        {
            CreatePool(1);
        }
        return pool.Dequeue();
    }

    public void PutPoolObj(GameObject obj)
    {
        pool.Enqueue(obj);
        obj.SetActive(false);
    }


}
