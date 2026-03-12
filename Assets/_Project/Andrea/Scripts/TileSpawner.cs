using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class TileSpawner : MonoBehaviour
{
    private float _nextSpawnZ = 0f;

    [SerializeField] private Transform _player;

    [SerializeField] private int _initialNumTiles = 4;
    [SerializeField] private float _tileLength = 50f;
    [SerializeField] private float _limitMeters = 150f;

    [SerializeField] private List<GameObject> tiles = new List<GameObject>();

    [SerializeField] private GameObject _StartingTileGO;
    [SerializeField] private Vector3 _resetPos = new Vector3(0f, 1f, -16f);


    private Vector3 _playerSartPos;
    private Quaternion _playerQuat;

    private bool isStartingGame = true;

    [SerializeField] private CinemachineVirtualCamera _virtualcam;

    private void Awake()
    {
        _playerSartPos = _player.position;
        _playerQuat = _player.rotation;
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

    public void DestroyStartingTile()
    {
        Destroy(_StartingTileGO);
    }

    private void ResetRunningGame()
    {
        foreach (GameObject tile in tiles)
        {
            TilePool.Instance.PutPoolObj(tile);
        }
        tiles.Clear();
        _nextSpawnZ = 0f;

        Vector3 Pos = _resetPos;
        //Vector3 Pos = new Vector3(0f, 1f, _playerSartPos.z + _tileLength * 0.5f);      
        Vector3 displacement = Pos - _player.position;

        _player.position = Pos;
        _player.rotation = _playerQuat;

        if (_virtualcam != null) _virtualcam.OnTargetObjectWarped(_player, displacement);
        _virtualcam.PreviousStateIsValid = false;

        CreateInitialTiles();
    }


    private void Update()
    {
        if (_player.position.z > tiles[0].transform.position.z && isStartingGame)
        {
            DestroyStartingTile();
            isStartingGame = false;
        }
        if (_player.position.z > tiles[0].transform.position.z + (_tileLength) && !isStartingGame)
        {
            HideBackTile();
            SpawnTile();
        }

        if (_player.position.z >= _limitMeters)
        {
            // Teleport Player to start position and restart tile
            ResetRunningGame();
        }

    }

}
