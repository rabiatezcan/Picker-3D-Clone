﻿using System.Collections;
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
        _isMovable = false;
    }
    public override void StartGame()
    {
        _isMovable = true;
    }  
    public override void Reload()
    {
    }
    public override void GameFail()
    {
        _isMovable = false;
        _inputController.RemoveInputs();
    }

    public override void GameSuccess()
    {
        _isMovable = false;
        _inputController.RemoveInputs();
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
    void Update()
    {
        UpdateMovement();
    }
    #endregion
}