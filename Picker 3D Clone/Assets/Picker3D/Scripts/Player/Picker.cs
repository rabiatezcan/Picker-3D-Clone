using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picker : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;
    [SerializeField] private float _horizontalSpeed;

    public void Movement(Vector2 inputPos)
    {
            Vector3 currentPos = transform.position;
            currentPos += inputPos.x * Time.deltaTime * _horizontalSpeed * Vector3.right;
            currentPos.x = Mathf.Clamp(currentPos.x, _minX, _maxX);

            transform.position = currentPos;
    }
}
