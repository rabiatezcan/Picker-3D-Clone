using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingTrigger : MonoBehaviour
{
    public void TriggerBehaviour()
    {
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
