using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picker : MonoBehaviour
{
    [SerializeField] private MovementSettings _movementSettings;

    public void Movement(Vector2 inputPos)
    {
        Vector3 currentPos = transform.position;
        currentPos += inputPos.x * Time.deltaTime * _movementSettings.HorizontalSpeed * Vector3.right;
        currentPos.x = Mathf.Clamp(currentPos.x, _movementSettings.MinX, _movementSettings.MaxX);

        transform.position = Vector3.Lerp(transform.position,currentPos, Time.deltaTime * _movementSettings.HorizontalSpeed);
    }
}
