using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Picker3D/Camera Settings")]
public class CameraSettings : ScriptableObject
{
    [SerializeField] private float _xAxisOffset;
    [SerializeField] private float _yAxisOffset;
    [SerializeField] private float _zAxisOffset;

    public float XAxisOffset => _xAxisOffset;
    public float YAxisOffset => _yAxisOffset;
    public float ZAxisOffset => _zAxisOffset;
}
