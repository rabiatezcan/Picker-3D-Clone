using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private List<ObjectHolder> _levelObjects;
    [SerializeField] private Transform _finishTransform;

    public Vector3 FinishPosition => _finishTransform.position;

    public void Build()
    {
        _levelObjects.ForEach(obj => obj.SetActive());
    }

    public void Remove()
    {
        _levelObjects.ForEach(obj => obj.SetDisactive());
    }

}
