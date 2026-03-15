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

    [SerializeField] private GameObject _startingTile;
    [SerializeField] private GameObject _currentTile;

    [SerializeField] private Vector3 _currentPosOnTile;

    private Vector3 _playerSartPos;
    private Quaternion _playerQuat;

    private bool isStartingGame = true;

    [SerializeField] private CinemachineVirtualCamera _virtualcam;

    private void Awake()
    {
        _playerSartPos = _player.position;
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

    public void HideBackTile()
    {
        GameObject tileToHide = tiles[0];
        tiles.RemoveAt(0); // RIMUOVO TILE
        TilePool.Instance.PutPoolObj(tileToHide); // E LO RIMETTO NEL POOL
    }

    public void DestroyStartingTile()
    {
        //if (_StartingTileGO != null)
        //{
        //    _StartingTileGO.SetActive(false); // NON DISTRUGGO IL TILE MA LO NASCONDO
        //}
    }

    private int GetCurrentTileIndex()
    {
        float playerZ = _player.position.z; // POSIZIONE ATTUALE DEL PLAYER

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
        Debug.Log($"CURRENT TILE INDEX : {_currentTile}");

        _currentPosOnTile = _player.position - _currentTile.transform.position; // MI SERVE PER EVITARE DI NOTARE LO STACCO DOPO IL TELEPORT

        float startZ = 0f;
        if (_StartingTileGO != null)
        {
            startZ = _StartingTileGO.transform.position.z + _tileLength; // nuova posizione del tile dal punto appena dopo lo start tile
        }

        Vector3 currentPlayerPos = _player.position;

        tiles.RemoveAt(currentTileIndex); // RIMUOVO il tile DALLA LISTA dove si trova ora  il player
        tiles.Insert(0, _currentTile);    // E PERò lo rimetto nella lista ma all'inizio della lista
        float z = startZ;
        for (int i = 0; i < tiles.Count; i++) // RISPAWNO ANCHE GLI ALTRI CHE ERO IN SEQUENZA DOPO IL TILE CORRENTE PER NON AVERE LO STACCO
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

        _nextSpawnZ = tiles[tiles.Count - 1].transform.position.z + _tileLength;
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
