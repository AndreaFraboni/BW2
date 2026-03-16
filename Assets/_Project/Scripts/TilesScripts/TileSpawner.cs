using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

public class TileSpawner : MonoBehaviour
{
    private float _nextSpawnZ = -31.8f;

    [SerializeField] private Transform _player;

    [SerializeField] private int _initialNumTiles = 4;
    [SerializeField] private float _tileLength = 20f;
    [SerializeField] private float _limitMeters = 150f;

    [SerializeField] private List<GameObject> tiles = new List<GameObject>();

    [SerializeField] private GameObject _StartingTileGO;
    [SerializeField] private GameObject _currentTile;

    [SerializeField] private Vector3 _currentPosOnTile;

    private Vector3 _playerStartPos;
    private Quaternion _playerQuat;

    [SerializeField] private bool _finalSequenceStarted = false;
    [SerializeField] private bool _finalSequenceSpawned = false;

    [SerializeField] private int _maxBiomaCycles = 3;
    [SerializeField] private int _TilesCycleCounter = 0;

    [SerializeField] private int _currentBioma = 1;

    [SerializeField] private CinemachineVirtualCamera _virtualcam;

    [SerializeField] private GameObject _finalTilePrefab;
    [SerializeField] private int _numLastTileabeforeFinalTile = 2;

    private GameObject _spawnedFinalTile;

    private GameObject _tile = null;

    private void Awake()
    {
        _playerStartPos = _player.position;
        _playerQuat = _player.rotation;
    }

    private void Start()
    {
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
        switch (_currentBioma)
        {
            case 1:
                _tile = Bioma1Pool.Instance.GetPoolObj();
                break;

            case 2:
                _tile = Bioma2Pool.Instance.GetPoolObj();
                break;

            case 3:
                _tile = Bioma3Pool.Instance.GetPoolObj();
                break;
        }
        
        _tile.transform.position = new Vector3(0f, 0f, _nextSpawnZ);
        _tile.transform.rotation = Quaternion.identity;
        _tile.SetActive(true);
        tiles.Add(_tile);
        _nextSpawnZ += _tileLength;
    }

    private void SpawnFinalTile()
    {
        if (_finalTilePrefab == null) return;

       // _spawnedFinalTile = Instantiate(_finalTilePrefab, new Vector3(0f, 0f, _nextSpawnZ), Quaternion.identity);
        switch (_currentBioma)
        {
            case 1:
                _spawnedFinalTile = Instantiate(Bioma1Pool.Instance.finalTilePrefab, new Vector3(0f, 0f, _nextSpawnZ), Quaternion.identity);
                break;

            case 2:
                _spawnedFinalTile = Instantiate(Bioma2Pool.Instance.finalTilePrefab, new Vector3(0f, 0f, _nextSpawnZ), Quaternion.identity);
                break;

            case 3:
                _spawnedFinalTile = Instantiate(Bioma3Pool.Instance.finalTilePrefab, new Vector3(0f, 0f, _nextSpawnZ), Quaternion.identity);
                break;
        }     

        tiles.Add(_spawnedFinalTile);

        _nextSpawnZ += _tileLength;
    }

    private void SpawnFinalSequence()
    {
        if (_finalSequenceSpawned) return;

        for (int i = 0; i < _numLastTileabeforeFinalTile; i++)
        {
            SpawnTile();
        }

        SpawnFinalTile();
        _finalSequenceSpawned = true;
    }

    public void HideBackTile()
    {
        if (tiles.Count == 0) return;

        GameObject tileToHide = tiles[0];
        tiles.RemoveAt(0);

        if (tileToHide == _spawnedFinalTile) return;

        //TilePool.Instance.PutPoolObj(tileToHide);
        switch (_currentBioma)
        {
            case 1:
                Bioma1Pool.Instance.PutPoolObj(tileToHide);
                break;

            case 2:
                Bioma2Pool.Instance.PutPoolObj(tileToHide);
                break;

            case3:
                Bioma3Pool.Instance.PutPoolObj(tileToHide);
                break;
        }
    }

    private int GetCurrentTileIndex()
    {
        float playerZ = _player.position.z;

        for (int i = tiles.Count - 1; i >= 0; i--)
        {
            if (playerZ >= tiles[i].transform.position.z)
            {
                return i;
            }
        }
        return 0;
    }

    private void ResetRunningGame()
    {
        if (tiles.Count == 0) return;

        int currentTileIndex = GetCurrentTileIndex();
        _currentTile = tiles[currentTileIndex];

        _currentPosOnTile = _player.position - _currentTile.transform.position;

        float startZ = 0f;
        if (_StartingTileGO != null)
        {
            startZ = _StartingTileGO.transform.position.z + _tileLength;
        }

        Vector3 currentPlayerPos = _player.position;

        tiles.RemoveAt(currentTileIndex);
        tiles.Insert(0, _currentTile);

        float z = startZ;
        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].transform.position = new Vector3(0f, 0f, z);
            tiles[i].transform.rotation = Quaternion.identity;
            z += _tileLength;
        }

        // riposiziona il player all'inizio sopra il tile corrente e posizionato dove si trovava sul tile
        Vector3 newPlayerPos = _currentTile.transform.position + _currentPosOnTile;
        _player.position = newPlayerPos;
        _player.rotation = _playerQuat;

        // riposiziona la telecamera 
        Vector3 displacement = newPlayerPos - currentPlayerPos;
        if (_virtualcam != null)
        {
            _virtualcam.OnTargetObjectWarped(_player, displacement);
            _virtualcam.PreviousStateIsValid = false;
        }

        _TilesCycleCounter++;
        
        _nextSpawnZ = tiles[tiles.Count - 1].transform.position.z + _tileLength; // next Z will be last tile spawned position.z + tile lenght
        
        if (_TilesCycleCounter >= _maxBiomaCycles)
        {
            _finalSequenceStarted = true;
            SpawnFinalSequence(); // avvio spawn ultima serie di tile del bioma e come ultimo tile mettiamo il finaltile del bioma
        }
    }

    private void Update()
    {
        if (tiles.Count > 0 && _player.position.z > tiles[0].transform.position.z + (_tileLength * 2))
        {
            HideBackTile();

            if (!_finalSequenceStarted)
            {
                SpawnTile();
            }
        }

        if (_player.position.z >= _limitMeters)
        {
            if (!_finalSequenceStarted)
            {
                ResetRunningGame();
            }
        }
    }
}