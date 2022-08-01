using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picker : MonoBehaviour
{
    [SerializeField] private MovementSettings _movementSettings;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private PickerPhysic _pickerPhysic;
    private Transform _transform;

    private bool _isStopped;

    public int BallCount => _pickerPhysic.Balls.Count;

    public void Initialize()
    {
        _transform = transform;
        _pickerPhysic.Initialize();
    }

    public void PassTheCheckPoint()
    {
        _isStopped = false;
    }

    public void Stop()
    {
        _rigidbody.velocity = Vector3.zero;
        _isStopped = true;
        _pickerPhysic.Balls.ForEach(ball => ball.Move());
    }
    public void Movement(Vector2 inputPos)
    {
        if (!_isStopped)
        {
            Vector3 currentPos = _transform.position;
            currentPos += new Vector3(inputPos.x * _movementSettings.HorizontalSpeed, 0f, _movementSettings.ForwardSpeed) * Time.deltaTime;
            currentPos.x = Mathf.Clamp(currentPos.x, _movementSettings.MinX, _movementSettings.MaxX);

            var newPos = Vector3.Lerp(_transform.position, currentPos, Time.deltaTime * _movementSettings.HorizontalSpeed);

            _rigidbody.MovePosition(newPos);
        }

    }

}
