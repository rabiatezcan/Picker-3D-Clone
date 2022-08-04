using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CheckPoint : PoolObject
{
    [SerializeField] private PhysicListener _physicListener;
    [SerializeField] private BallCountCheckArea _checkArea;
    [SerializeField] private PlatformGround _ground;
    [SerializeField] private Barrier _barrier;

    private GameEnums.CheckPointStates _currentState;
    private Picker _picker;

    public int TargetBallValue
    {
        get => _checkArea.TargetCount;
        set
        {
            _checkArea.TargetCount = value;
        }
    }
    public override void SetActive()
    {
        base.SetActive();
        Initialize();

    }

    public override void Dismiss()
    {
        SetDefault();
        base.Dismiss();
    }

    private void Initialize()
    {
        _currentState = GameEnums.CheckPointStates.Idle;
        _checkArea.Initialize();
        _ground.Initialize();
        _barrier.Initialize();
    }

    private void SetDefault()
    {
        _currentState = GameEnums.CheckPointStates.Idle;
        _ground.SetDefault();
        _checkArea.SetDefault();
        _barrier.SetDefault();
    }
    public void StopPicker()
    {
        _picker = _physicListener.ContactCollider.GetComponentInParent<Picker>();

        if (_picker != null)
        {
            _checkArea.PickerBallCount = _picker.BallCount;
            _picker.Stop();
        }

        _currentState = GameEnums.CheckPointStates.Counting;
    }

    #region States Update Methods
    private void CountingStateUpdate()
    {
        if (_checkArea.IsCountingStateFinish())
        {
            if (_checkArea.IsSuccess())
            {
                _currentState = GameEnums.CheckPointStates.PlatformRaise;
            }
            else
            {
                _currentState = GameEnums.CheckPointStates.Idle;

                GameManager.Instance.GameFail();
            }
        }
    }

    private void PlatformRaiseStateUpdate()
    {
        _ground.RaiseGround();

        if (_ground.IsRaisingCompleted)
        {
            _currentState = GameEnums.CheckPointStates.Barrier;
        }
    }

    private void BarrierStateUpdate()
    {
        _barrier.OpenBarrier();

        if (_barrier.IsOpen)
        {
            _picker.PassTheCheckPoint();
            _currentState = GameEnums.CheckPointStates.Idle;
        }
    }

    #endregion

    private void Update()
    {
        if (_currentState == GameEnums.CheckPointStates.Counting)
            CountingStateUpdate();
        if (_currentState == GameEnums.CheckPointStates.PlatformRaise)
            PlatformRaiseStateUpdate();
        if (_currentState == GameEnums.CheckPointStates.Barrier)
            BarrierStateUpdate();
    }

}
