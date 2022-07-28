using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private Vector2 _previousPos, _currentPos, _deltaPos;
    private float _sensitivity = .1f ;

    public Vector2 DeltaPos => _deltaPos;

    public void RemoveInputs()
    {
        _deltaPos = Vector2.zero;
    }

    public void Update()
    {
        InputUpdate();
    }

    private void InputUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _currentPos = Input.mousePosition;
            _previousPos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            _currentPos = Input.mousePosition;
            _deltaPos = (_currentPos - _previousPos) * _sensitivity;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _deltaPos = Vector3.zero;
        }
    }
}
