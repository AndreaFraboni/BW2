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
    [SerializeField] private GameObject _currentTile;

    [SerializeField] private Vector3 _currentPosOnTile;

    private Vector3 _playerStartPos;
    private Quaternion _playerQuat;

    [SerializeField] private bool _finalSequenceStarted = false;
    [SerializeField] private bool _finalSequenceSpawned = false;

    [SerializeField] private int _maxBiomaCycles = 3;
    [SerializeField] private int _currentNumBiomaCycle = 0;

    [SerializeField] private CinemachineVirtualCamera _virtualcam;

    [SerializeField] private GameObject _finalTilePrefab;
    [SerializeField] private int _numLastTileabeforeFinalTile = 2;

    private GameObject _spawnedFinalTile;

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
        GameObject tile = TilePool.Instance.GetPoolObj();

        tile.transform.position = new Vector3(0f, 0f, _nextSpawnZ);
        tile.transform.rotation = Quaternion.identity;
        tile.SetActive(true);

        tiles.Add(tile);
        _nextSpawnZ += _tileLength;
    }

    private void SpawnFinalTile()
    {
        if (_finalTilePrefab == null) return;

        _spawnedFinalTile = Instantiate(_finalTilePrefab, new Vector3(0f, 0f, _nextSpawnZ), Quaternion.identity);

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

        TilePool.Instance.PutPoolObj(tileToHide);
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

        Vector3 newPlayerPos = _currentTile.transform.position + _currentPosOnTile;
        _player.position = newPlayerPos;
        _player.rotation = _playerQuat;

        Vector3 displacement = newPlayerPos - currentPlayerPos;
        if (_virtualcam != null)
        {
            _virtualcam.OnTargetObjectWarped(_player, displacement);
            _virtualcam.PreviousStateIsValid = false;
        }

        _currentNumBiomaCycle++;
        
        _nextSpawnZ = tiles[tiles.Count - 1].transform.position.z + _tileLength; // next Z will be last tile spawned position.z + tile lenght
        
        if (_currentNumBiomaCycle >= _maxBiomaCycles)
        {
            _finalSequenceStarted = true;
            SpawnFinalSequence(); // avvio spawn ultima serie di tile e come ultimo tile il final tile del bioma
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