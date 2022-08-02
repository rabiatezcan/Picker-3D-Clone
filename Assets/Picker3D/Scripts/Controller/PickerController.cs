using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickerController : Controller
{
    [SerializeField] private Picker _picker;

    private InputController _inputController;
    private bool _isMovable;

    #region States
    public override void Initialize(GameManager gameManager)
    {
        _inputController = new InputController();
        _picker.Initialize();
        _isMovable = false;
    }
    public override void StartGame()
    {
        _isMovable = true;
        _inputController.OnMouseButtonDown += _picker.ListenTapInput;
    }  

    public override void Reload()
    {
    }
    public override void GameFail()
    {
        _isMovable = false;
        _inputController.RemoveInputs();
        _inputController.OnMouseButtonDown -= _picker.ListenTapInput;

    }

    public override void GameSuccess()
    {
        _isMovable = false;
        _inputController.RemoveInputs();
        _inputController.OnMouseButtonDown -= _picker.ListenTapInput;
    }
    #endregion

    #region Movement
    public void UpdateMovement()
    {
        if (_isMovable)
        {
            _inputController.Update();

            _picker.Movement(_inputController.DeltaPos);
        }
    }
    void FixedUpdate()
    {
        UpdateMovement();
    }
    #endregion
}
