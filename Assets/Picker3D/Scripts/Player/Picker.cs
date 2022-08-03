using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picker : MonoBehaviour
{
    [SerializeField] private MovementSettings _movementSettings;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private PickerPhysic _pickerPhysic;
    private Transform _transform;

    private float _rampEndForceMultiplier = 5f;
    private bool _isStopped;
    private bool _isLevelFinished;

    public int BallCount => _pickerPhysic.Balls.Count;

    #region States
    public void Initialize()
    {
        _transform = transform;
        _pickerPhysic.Initialize();
        _pickerPhysic.OnLevelFinish += LevelFinishBehaviour;
        _pickerPhysic.OnRampEnd += AddForce;
    }

    public void Reload()
    {
        _rigidbody.isKinematic = true;
    }

    #endregion

    #region CheckPoint Behaviours
    public void PassTheCheckPoint()
    {
        _isStopped = false;
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
        _isLevelFinished = true;
    }

    public void ListenTapInput()
    {
        if (_isLevelFinished)
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
        _isLevelFinished = false;
    }
    #endregion

    #region Movement
    public void Movement(Vector2 inputPos)
    {
        if (!_isStopped && !_isLevelFinished)
        {
            Vector3 currentPos = _transform.position;
            currentPos += new Vector3(inputPos.x * _movementSettings.HorizontalSpeed, 0f, _movementSettings.ForwardSpeed) * Time.deltaTime;
            currentPos.x = Mathf.Clamp(currentPos.x, _movementSettings.MinX, _movementSettings.MaxX);

            var newPos = Vector3.Lerp(_transform.position, currentPos, Time.deltaTime * _movementSettings.HorizontalSpeed);

            _rigidbody.MovePosition(newPos);
        }
        if (_isLevelFinished)
        {
            var currentPos = _transform.position;
            currentPos += new Vector3(0f, 0f, _movementSettings.ForwardSpeed / .8f * Time.deltaTime);
            var newPos = Vector3.Lerp(_transform.position, currentPos, Time.deltaTime /.8f * _movementSettings.HorizontalSpeed);

            _rigidbody.MovePosition(newPos);
        }

    }
    #endregion

}
