﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BallCountCheckArea : MonoBehaviour
{
    [SerializeField] private TextMeshPro _ballCountText;
    [SerializeField] private PhysicListener _physicListener;
    private int _pickerBallCount;
    private int _ballCount;
    private int _targetCount = 5;


    public int PickerBallCount
    {
        get => PickerBallCount; 
        set
        {
            _pickerBallCount = value;
        }
    }
    public void Initialize()
    {
        _ballCount = 0;
        UpdateBallCountText();
    }

    public void BallContact()
    {
        IncreaseBallCount();
        UpdateBallCountText();

        var ball = _physicListener.ContactCollider.GetComponentInParent<Ball>();
        ball.ContactDismiss();
    }

    private void IncreaseBallCount() => _ballCount++;

    private void UpdateBallCountText()
    {
        _ballCountText.text = _targetCount + " / " + _ballCount;
    }

    public bool IsCountingStateFinish()
    {
        if (_pickerBallCount == _ballCount)
        {
            return true;
        }

        return false;
    }

    public bool IsSuccess()
    {
        if (_ballCount >= _targetCount)
            return true;
        else
            return false;
    }

}
