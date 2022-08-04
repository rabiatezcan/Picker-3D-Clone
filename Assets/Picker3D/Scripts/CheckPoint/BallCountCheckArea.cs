using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BallCountCheckArea : MonoBehaviour
{
    [SerializeField] private TextMeshPro _ballCountText;
    [SerializeField] private PhysicListener _physicListener;
    private int _pickerBallCount;
    private int _ballCount;
    private int _targetCount;

    public int TargetCount 
    { 
        get => _targetCount; 
        set { 
            _targetCount = value; 
        } 
    }

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

    public void SetDefault()
    {
        _ballCountText.text = "0";
        _ballCount = 0;
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

        if (_ballCount >= _pickerBallCount)
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
