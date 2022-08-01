using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private Vector3 _rotateAxis;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _rotateDirection;

    private void Update()
    {
        if(gameObject.activeInHierarchy)
            transform.Rotate(_rotateAxis * _rotateDirection, _rotateSpeed * Time.deltaTime * CONSTANTS.ROTATEOBJECT_MULTIPLIER);
    }

}
