using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum CurrentLane { LEFTLANE, RIGHTLANE, MIDLANE }

    [Header("Player Settings")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _maxSpeedIncrease;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _timeGap;
    [SerializeField] private CurrentLane _currentLane;

    [Header("Lane Settings")]
    [SerializeField] private float _laneOffset = 5f;
    [SerializeField] private float _laneChangeSpeed = 15f;

    private float _currentSpeed;
    private float _timer;

    private Rigidbody _rb;
    private GroundCheckAlessio _gc;
    private LifeController _lc;
    private AnimationParamHandler _animationParamHandler;

    public AnimationParamHandler AnimationParamHandler => _animationParamHandler;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _gc = GetComponentInChildren<GroundCheckAlessio>();
        _lc = GetComponentInChildren<LifeController>();
        _animationParamHandler = GetComponent<AnimationParamHandler>();
        _currentLane = CurrentLane.MIDLANE;
        _currentSpeed = _speed;
        PlayerManager.Instance.SetPlayer(this,_lc);
       
    }

    private void Update()
    {
        //Debug.Log(_currentSpeed);

        IncreseSpeed();
        IncreaseMaxSpeedOnTimer();

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
        Vector3 forward = new Vector3(0, 0, _currentSpeed);

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

    private void Jump()
    {
        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        _animationParamHandler.Jump();
    }

    public void ActivateSlow(float duration , float slowMultiplier)
    {
        StartCoroutine(SlowCoroutine(duration, slowMultiplier));
    }

    private IEnumerator SlowCoroutine(float duration , float slowMultiplier)
    {
        float currentSpeed = _currentSpeed;
        _currentSpeed *= slowMultiplier;
        yield return new WaitForSeconds(duration);
        _currentSpeed = currentSpeed;

    }

    private void IncreseSpeed()
    {
        _currentSpeed = Mathf.MoveTowards(_currentSpeed, _maxSpeed, _acceleration * Time.deltaTime);
    }

    public void IncreseMaxSpeed()
    {
        _maxSpeed += _maxSpeedIncrease;
    }

    private void IncreaseMaxSpeedOnTimer()
    {
        
        _timer += Time.deltaTime;
        if (_timer >= _timeGap)
        {
            _timer = 0f;
            IncreseMaxSpeed();
        }
    }
}

