using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Picker3D/Movement Settings", order = 2)]

public class MovementSettings : ScriptableObject
{
    [Header("Movement Parameters")]
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;
    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private float _forwardSpeed;

    public float MinX => _minX;
    public float MaxX => _maxX;
    public float HorizontalSpeed => _horizontalSpeed;
    public float ForwardSpeed => _forwardSpeed;
}
