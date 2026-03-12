using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TileSpawner : MonoBehaviour
{
    private float _nextSpawnZ = 0f;

    [SerializeField] private Transform _player;

    [SerializeField] private int _initialNumTiles = 4;
    [SerializeField] private float _tileLength = 50f;
    [SerializeField] private float _limitMeters = 150f;

    [SerializeField] private List<GameObject> tiles = new List<GameObject>();

    private Vector3 _playerSartPos;

    [SerializeField] private CinemachineVirtualCamera _virtualcam;

    private void Awake()
    {
        _playerSartPos = _player.position;
        CreateInitialTiles();
    }

    private void CreateInitialTiles()
    {
        // Starting to spawn intial tiles
        for (int i = 0; i < _initialNumTiles; i++)
        {
            SpawnTile();
        }
    }

    public void SpawnTile()
    {
        GameObject tile = TilePool.Instance.GetPoolObj();
        tile.transform.position = new Vector3(0F, 0F, _nextSpawnZ);
        tile.transform.rotation = Quaternion.identity;
        tile.SetActive(true);
        tiles.Add(tile);
        _nextSpawnZ += _tileLength;
    }


    public void HideBackTile()
    {
        GameObject tileToHide = tiles[0];
        tiles.RemoveAt(0);
        TilePool.Instance.PutPoolObj(tileToHide);
    }

    private void ResetRunningGame()
    {
        //foreach (GameObject tile in tiles)
        //{
        //    TilePool.Instance.PutPoolObj(tiles[tiles.Count - 1]);
        //}
        //tiles.Clear();
        //_nextSpawnZ = 0;
        //_player.position = _playerSartPos;
        //CreateInitialTiles();
    }

    private void Update()
    {
        if (_player.position.z > tiles[0].transform.position.z + (_tileLength))
        {
            HideBackTile();
            SpawnTile();
        }

        if (_player.position.z >= _limitMeters)
        {
            // Teleport Player to start position and restart tile
            //ResetRunningGame();
        }

    }

}
