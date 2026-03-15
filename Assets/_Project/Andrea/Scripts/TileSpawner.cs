using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class TileSpawner : MonoBehaviour
{
    private float _nextSpawnZ = -31.8f;

    [SerializeField] private Transform _player;

    [SerializeField] private int _initialNumTiles = 4;
    [SerializeField] private float _tileLength = 20f;
    [SerializeField] private float _limitMeters = 150f;

    [SerializeField] private List<GameObject> tiles = new List<GameObject>();

    [SerializeField] private GameObject _StartingTileGO;
    [SerializeField] private Vector3 _resetPos = new Vector3(0f, 1f, -50f);

    [SerializeField] private GameObject _currentTile;
    [SerializeField] private GameObject _startingTile;

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
        if (_StartingTileGO != null)
        {
            _StartingTileGO.SetActive(false);
        }
    }

    private void ResetRunningGame()
    {
        //_currentTile = tiles[0];
        //_startingTile = _currentTile;

        //for (int i = 1; i < tiles.Count; i++)
        //{
        //    TilePool.Instance.PutPoolObj(tiles[i]);
        //}

        //tiles.Clear();

        //_startingTile.transform.position = new Vector3(0f, 0f, 0f);
        //_startingTile.transform.rotation = Quaternion.identity;
        //_startingTile.SetActive(true);

        //_nextSpawnZ = _tileLength;

        //Vector3 pos = _resetPos;
        //Vector3 displacement = pos - _player.position;

        //_player.position = pos;
        //_player.rotation = _playerQuat;

        //if (_virtualcam != null)
        //{
        //    _virtualcam.OnTargetObjectWarped(_player, displacement);
        //    _virtualcam.PreviousStateIsValid = false;
        //}

        //while (tiles.Count < _initialNumTiles)
        //{
        //    SpawnTile();
        //}

        //isStartingGame = true;
    }

    private void Update()
    {
        if (_player.position.z > tiles[0].transform.position.z + (_tileLength * 0.5f) && isStartingGame)
        {
            DestroyStartingTile();
            isStartingGame = false;
        }
        if (_player.position.z > tiles[0].transform.position.z + (_tileLength * 2) && !isStartingGame)
        {
            HideBackTile();
            SpawnTile();
        }

        if (_player.position.z >= _limitMeters)
        {
            ResetRunningGame();
        }

    }

}
