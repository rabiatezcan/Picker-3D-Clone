using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    private bool _isTaken;
    public bool IsTaken => _isTaken;


    public virtual void SetActive()
    {
        _isTaken = true;
        gameObject.SetActive(true);
    }

    public virtual void SetActiveWithPosition(Vector3 position)
    {
        transform.position = position;
        SetActive();
    }   
    public virtual void Dismiss()
    {
        _isTaken = false;
        gameObject.SetActive(false);
    }
}
