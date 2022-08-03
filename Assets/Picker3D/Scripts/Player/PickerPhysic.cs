using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickerPhysic : MonoBehaviour
{
    public Action OnLevelFinish;
    public Action OnRampEnd;
    [SerializeField] private PhysicListener _physicListener;
    [SerializeField] private WingHandler _wingHandler;

    private List<Ball> _balls;

    public List<Ball> Balls => _balls;

    public void Initialize()
    {
        _balls = new List<Ball>();
    }
    public void WingsDismiss()
    {
        _wingHandler.Hide();
    }
    
    public void TriggerEnterBehaviour()
    {
        var other = _physicListener.ContactCollider;
        if(other.CompareTag("Ball"))
        {
            AddBall(other.GetComponentInParent<Ball>());
        }
        if (other.CompareTag("WingTrigger"))
        {
            var wingTrigger = other.GetComponentInParent<WingTrigger>();

            if (wingTrigger != null)
                wingTrigger.TriggerBehaviour();

            _wingHandler.ShowSequence();
        }
        if (other.CompareTag("LevelFinish"))
        {
            OnLevelFinish?.Invoke();
        }

        if (other.CompareTag("RampEnd"))
        {
            OnRampEnd?.Invoke();
        }
    }

    public void TriggerExitBehaviour()
    {
        var other = _physicListener.ContactCollider;
        if (other.CompareTag("Ball"))
        {
            RemoveBall(other.GetComponentInParent<Ball>());
        }

    }
    private void AddBall(Ball ball)
    {
        if (ball != null)
            _balls.Add(ball);
    }

    private void RemoveBall(Ball ball)
    {
        if (ball != null)
            _balls.Remove(ball);
    }


}
