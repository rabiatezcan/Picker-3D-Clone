using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picker : MonoBehaviour
{
    [SerializeField] private MovementSettings _movementSettings;
    [SerializeField] private Rigidbody _rigidbody;
    private Transform _transform;

    public void Initialize()
    {
        _transform = transform;
    }

    public void Movement(Vector2 inputPos)
    {
        Vector3 currentPos = _transform.position;
        currentPos += new Vector3(inputPos.x * _movementSettings.HorizontalSpeed, 0f, _movementSettings.ForwardSpeed) * Time.deltaTime;
        currentPos.x = Mathf.Clamp(currentPos.x, _movementSettings.MinX, _movementSettings.MaxX);

        var newPos = Vector3.Lerp(_transform.position, currentPos, Time.deltaTime * _movementSettings.HorizontalSpeed);

        _rigidbody.MovePosition(newPos);
    }

}
