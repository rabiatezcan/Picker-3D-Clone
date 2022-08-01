using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointGround : MonoBehaviour
{
    [SerializeField] private Vector3 _raisingPosition;
    [SerializeField] private float _raisingDuration;

    private Transform _transform;
    private Vector3 _defaultPos;
    private bool _isRaisingCompleted;
    private float _time;

    public bool IsRaisingCompleted => _isRaisingCompleted;
    public void Initialize()
    {
        _transform = transform;
        _defaultPos = _transform.localPosition;
        SetDefault();
       
    }

    public void RaiseGround()
    {
        if (_time < 1)
        {
            _transform.localPosition = Vector3.Lerp(_defaultPos, _raisingPosition, _time);

            _time += Time.deltaTime / _raisingDuration;
        }
        else
        {
            _transform.localPosition = _raisingPosition;
            _isRaisingCompleted = true;
            _time = 0f; 
        }
    }

    public void SetDefault()
    {
        _transform.localPosition = _defaultPos;
        _isRaisingCompleted = false;
        _time = 0f;
    }





}
