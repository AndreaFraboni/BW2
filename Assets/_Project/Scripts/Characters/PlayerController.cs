using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum CurrentLane { LEFTLANE, RIGHTLANE, MIDLANE }

    
    [Header("Player Settings")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce;
    [SerializeField] private CurrentLane _currentLane;

    [Header("Lane Settings")]
    [SerializeField] private float _laneOffset = 5f;
    [SerializeField] private float _laneChangeSpeed = 15f;

    private Rigidbody _rb;
    private GroundCheckAlessio _gc;
    private AnimationParamHandler _animationParamHandler;

    private int _cyberScore;
    private int _naturalScore;
    private int _blackWhiteScore;

    public AnimationParamHandler AnimationParamHandler => _animationParamHandler;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _gc = GetComponentInChildren<GroundCheckAlessio>();
        _animationParamHandler = GetComponent<AnimationParamHandler>();
        _currentLane = CurrentLane.MIDLANE;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && _gc.IsGrounded)
            Jump();


        if (Input.GetKeyDown(KeyCode.A))
        {
            if (_currentLane == CurrentLane.MIDLANE)
                _currentLane = CurrentLane.LEFTLANE;
            else if (_currentLane == CurrentLane.RIGHTLANE)
                _currentLane = CurrentLane.MIDLANE;
            AnimationParamHandler.ChangeLaneL();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (_currentLane == CurrentLane.MIDLANE)
                _currentLane = CurrentLane.RIGHTLANE;
            else if (_currentLane == CurrentLane.LEFTLANE)
                _currentLane = CurrentLane.MIDLANE;
            AnimationParamHandler.ChangeLaneR();
        }
    }


    private void FixedUpdate()
    {
        Vector3 forward = new Vector3(0, 0, _speed);

        float targetX = 0;

        switch (_currentLane)
        {
            case CurrentLane.LEFTLANE:
                targetX = -_laneOffset;
                break;
            case CurrentLane.MIDLANE:
                targetX = 0;
                break;
            case CurrentLane.RIGHTLANE:
                targetX = _laneOffset;
                break;
        }

        float newX = Mathf.MoveTowards(_rb.position.x, targetX, _laneChangeSpeed * Time.fixedDeltaTime);

        Vector3 newPos = new Vector3(newX, _rb.position.y, _rb.position.z);

        _rb.MovePosition(newPos + forward * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Coins>(out var coins))
        {
            coins.Collect(this);
        }
    }

    private void Jump()
    {
        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        _animationParamHandler.Jump();
    }

    public void AddScore(int amount, Coins.coinType coin)
    {
        switch (coin)
        {
            case Coins.coinType.CYBERCOIN:
                _cyberScore += amount;
                break;
            case Coins.coinType.NATURALCOIN:
                _naturalScore += amount;
                break;
            case Coins.coinType.BLACKWHITECOIN:
                _blackWhiteScore += amount;
                break;
        }
        Debug.Log("raccolto" + coin + _cyberScore + _naturalScore + _blackWhiteScore);
    }
}

