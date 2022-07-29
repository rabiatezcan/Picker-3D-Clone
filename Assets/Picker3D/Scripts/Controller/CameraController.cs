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
    public override void Initialize(GameManager gameManager)
    {
        _transform = transform;
        _canFollow = false;
    }
    public override void Reload()
    {
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

    #region Follow
    private void Follow()
    {
        var currentPos = _followObjTransform.position;

        if (_canFollow)
          _transform.position = new Vector3(0f, currentPos.y + _cameraSettings.YAxisOffset, currentPos.z + _cameraSettings.ZAxisOffset);
    }

    private void LateUpdate()
    {
        Follow();
    }
    #endregion




}
