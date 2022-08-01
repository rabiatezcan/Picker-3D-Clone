using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicListener : MonoBehaviour
{
    public string CompareTagName;

    public UnityEvent TriggerEnterCallback;
    public UnityEvent TriggerExitCallback;

    private Collider contactCollider;
    public Collider ContactCollider => contactCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == CompareTagName || CompareTagName == "")
        {
            contactCollider = other;
            TriggerEnterCallback.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == CompareTagName || CompareTagName == "")
        {
            contactCollider = other;
            TriggerExitCallback.Invoke();
        }
    }

}
