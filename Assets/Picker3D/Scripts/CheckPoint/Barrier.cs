using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    [SerializeField] private Transform _rightBarrier;
    [SerializeField] private Transform _leftBarrier;
    [SerializeField] private float _openingDuration;
    [SerializeField] private Vector3 _defaultRotation;

    private float _time;
    private bool _isOpen;

    public bool IsOpen => _isOpen;

    public void Initialize()
    {
        SetDefault();
    }
    public void SetDefault()
    {
        _isOpen = false;
        _rightBarrier.transform.rotation = Quaternion.Euler(_defaultRotation);
        _leftBarrier.transform.rotation = Quaternion.Euler(_defaultRotation);
    }
    public void OpenBarrier()
    {
        if (_time < 1f)
        {
            _rightBarrier.rotation = Quaternion.Lerp(Quaternion.Euler(_defaultRotation), Quaternion.Euler(90f, 90f, 90f), _time);
            _leftBarrier.rotation = Quaternion.Lerp(Quaternion.Euler(_defaultRotation), Quaternion.Euler(-90f, 90f, 90f), _time);

            _time += Time.deltaTime / _openingDuration;
        }
        else
        {
            _isOpen = true;
            _time = 0;
        }
    }


}
