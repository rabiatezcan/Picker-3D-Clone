using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickerPhysic : MonoBehaviour
{
    [SerializeField] private PhysicListener _physicListener;
    
    private List<Ball> _balls;

    public List<Ball> Balls => _balls;

    public void Initialize()
    {
        _balls = new List<Ball>();
    }

    private void AddBall(Ball ball)
    {
        if(ball != null)
            _balls.Add(ball);
    }

    private void RemoveBall(Ball ball)
    {
        if (ball != null)
            _balls.Remove(ball);
    }

    public void TriggerEnterBehaviour()
    {
        var other = _physicListener.ContactCollider;
        if(other.CompareTag("Ball"))
        {
            AddBall(other.GetComponentInParent<Ball>());
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
  
}
