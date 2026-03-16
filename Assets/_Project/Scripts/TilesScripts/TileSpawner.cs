using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using UnityEditor.ShaderGraph.Internal;

public class TileSpawner : MonoBehaviour
{
    public static TileSpawner Instance { get; private set; }

    private float _nextSpawnZ = -31.8f;
    private float _startOffset = 5.0f;

    [SerializeField] private Transform _player;

    [SerializeField] private int _initialNumTiles = 5;
    [SerializeField] private float _tileLength = 20f;
    [SerializeField] private float _limitMeters = 150f;

    [SerializeField] private List<GameObject> tiles = new List<GameObject>();

    [SerializeField] private GameObject _Bioma1StartTile;
    [SerializeField] private GameObject _Bioma2StartTile;
    [SerializeField] private GameObject _Bioma3StartTile;
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
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

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

            case 3:
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
        if (_Bioma1StartTile != null)
        {
            startZ = _Bioma1StartTile.transform.position.z + _tileLength;
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

    private void TeleportPlayerToStart()
    {
        float posZ = tiles[0].transform.position.z + _startOffset;

        Vector3 newPlayerPos = new Vector3(0, 0, posZ);
        _player.position = newPlayerPos;
        _player.rotation = _playerQuat;
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

    public void ChangeBioma()
    {
        foreach (GameObject tile in tiles)
        {
            if (tile == _spawnedFinalTile) continue;

            switch (_currentBioma)
            {
                case 1:
                    Bioma1Pool.Instance.PutPoolObj(tile);
                    _Bioma1StartTile.SetActive(false);
                    break;

                case 2:
                    Bioma2Pool.Instance.PutPoolObj(tile);
                    _Bioma2StartTile.SetActive(false);
                    break;

                case 3:
                    Bioma3Pool.Instance.PutPoolObj(tile);
                    _Bioma3StartTile.SetActive(false);
                    break;
            }
        }

        tiles.Clear();

        if (_spawnedFinalTile != null)
        {
            Destroy(_spawnedFinalTile);
        }

        _TilesCycleCounter = 0;
        _finalSequenceStarted = false;
        _finalSequenceSpawned = false;

        _currentBioma++;

        switch (_currentBioma)
        {
            case 1:
                _Bioma1StartTile.SetActive(true);
                break;

            case 2:
                _Bioma2StartTile.SetActive(true);
                break;

            case 3:
                _Bioma3StartTile.SetActive(true);
                break;
        }

        CreateInitialTiles();
        TeleportPlayerToStart();
    }

}