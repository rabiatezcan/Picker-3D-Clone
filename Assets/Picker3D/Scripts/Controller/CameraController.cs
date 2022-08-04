using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Controller
{
    [SerializeField] private CameraSettings _cameraSettings;
    [SerializeField] private Transform _followObjTransform;
    private Transform _transform;
    private bool _canFollow; 
    #region States
    public override void Initialize()
    {
        _transform = transform;
        _canFollow = false;
    }
    public override void Reload()
    {
        SetPosition(_followObjTransform.position);
    }

    public override void StartGame()
    {
        _canFollow = true;
    }
    public override void GameFail()
    {
        _canFollow = false;
    }

    public override void GameSuccess()
    {
        _canFollow = false;
    }

    #endregion

    private void SetPosition(Vector3 newPos)
    {
        _transform.position = new Vector3(0f, newPos.y + _cameraSettings.YAxisOffset, newPos.z + _cameraSettings.ZAxisOffset);
    }
    #region Follow
    private void Follow()
    {
        if (_canFollow)
            SetPosition(_followObjTransform.position);
    }

    private void LateUpdate()
    {
        Follow();
    }
    #endregion




}
