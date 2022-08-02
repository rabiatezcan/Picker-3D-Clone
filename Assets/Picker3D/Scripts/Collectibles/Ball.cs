using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : PoolObject
{
    [SerializeField] private Rigidbody _rigidbody;
    public void Move()
    {
        _rigidbody.AddForce(Vector3.forward * CONSTANTS.BALL_FORCE_MULTIPLIER, ForceMode.VelocityChange);
    }

    public void ContactDismiss()
    {
        StartCoroutine(DismissDelay(.5f));
    }
    public IEnumerator DismissDelay(float durationInSeconds)
    {
        yield return new WaitForSeconds(durationInSeconds);
        Dismiss();
    }
}
