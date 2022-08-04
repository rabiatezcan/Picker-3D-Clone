using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picker : MonoBehaviour
{
    public Action OnPassCheckPoint;
    [SerializeField] private MovementSettings _movementSettings;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private PickerPhysic _pickerPhysic;

    private Transform _transform;
    private Vector3 _startPos;
    private float _rampEndForceMultiplier = 5f;
    private bool _isStopped;
    private bool _isLevelCompleted;
    private bool _isMovingForNewStartPos;

    public PickerPhysic PickerPhysic => _pickerPhysic;
    public int BallCount => _pickerPhysic.Balls.Count;

    public Vector3 StartPos { get => _startPos; set => _startPos = value; }

    #region States
    public void Initialize()
    {
        _transform = transform;
        _pickerPhysic.Initialize();
        _pickerPhysic.OnRampStart += LevelFinishBehaviour;
        _pickerPhysic.OnRampEnd += AddForce;
        _pickerPhysic.OnLevelFinish += MoveToLevelStartPosition;
    }

    private void MoveToLevelStartPosition()
    {
        _isLevelCompleted = true;
        _isMovingForNewStartPos = true;
    }

    public void Reload()
    {
        _transform.position = StartPos;
        _rigidbody.isKinematic = true;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.inertiaTensor = Vector3.one;
        _isStopped = false;
        _isLevelCompleted = false;
    }

    #endregion

    #region CheckPoint Behaviours
    public void PassTheCheckPoint()
    {
        _isStopped = false;
        OnPassCheckPoint?.Invoke();
    }

    public void Stop()
    {
        _pickerPhysic.WingsDismiss();
        _rigidbody.velocity = Vector3.zero;
        _isStopped = true;
        _pickerPhysic.Balls.ForEach(ball => ball.Move());
    }
    #endregion

    #region LevelEnd Behaviours
    private void LevelFinishBehaviour()
    {
        _rigidbody.isKinematic = false;
        _isStopped = true;
        _isLevelCompleted = true;
    }

    public void ListenTapInput()
    {
        if (_isLevelCompleted)
            IncreaseLevelEndForceValue();
    }
    private void IncreaseLevelEndForceValue()
    {
        _rampEndForceMultiplier += CONSTANTS.LEVEL_FINISH_FORCE_VALUE_PER_TAP;
    }
    private void AddForce()
    {
        _rigidbody.AddForce((Vector3.forward + Vector3.up) * _rampEndForceMultiplier, ForceMode.VelocityChange);
        _rigidbody.velocity = Vector3.zero;
        _isLevelCompleted = false; 
    }
    #endregion

    #region Movement
    public void Movement(Vector2 inputPos)
    {
        if (!_isStopped && !_isLevelCompleted)
        {
            Vector3 currentPos = _transform.position;
            currentPos += new Vector3(inputPos.x * _movementSettings.HorizontalSpeed, 0f, _movementSettings.ForwardSpeed) * Time.deltaTime;
            currentPos.x = Mathf.Clamp(currentPos.x, _movementSettings.MinX, _movementSettings.MaxX);

            var newPos = Vector3.Lerp(_transform.position, currentPos, Time.deltaTime * _movementSettings.HorizontalSpeed);

            _rigidbody.MovePosition(newPos);
        }
        if (_isLevelCompleted)
        {
            var currentPos = _transform.position;
            currentPos += new Vector3(0f, 0f, _movementSettings.ForwardSpeed / .5f * Time.deltaTime);
            var newPos = Vector3.Lerp(_transform.position, currentPos, Time.deltaTime /.8f * _movementSettings.HorizontalSpeed);
            if(_isMovingForNewStartPos && newPos.z >= _startPos.z)
            {
                newPos = StartPos;
               
                GameManager.Instance.GameSuccess();
            }
            _rigidbody.MovePosition(newPos);
        }

    }
    #endregion

}
