using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [SerializeField] private Transform _startTilePosition;

    public bool isStartGame = true;

    private void Awake()
    {
        
        if (isStartGame)
        {
            SpawnTile(_startTilePosition);
        }

    }


    public void SpawnTile(Transform Pos)
    {
        GameObject Tile = TilePool.Instance.GetPoolObj();
        GameObject TilePrefab = Instantiate(Tile, Pos.position, Quaternion.identity);
        TilePrefab.SetActive(true);
    }


}
