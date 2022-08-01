using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingHandler : MonoBehaviour
{
    [SerializeField] private float _wingUsageDuration;

    public void ShowSequence()
    {
        Show();
        StartCoroutine(WingUsageCoroutine(_wingUsageDuration));
    }

    private IEnumerator WingUsageCoroutine(float durationInSeconds)
    {
        yield return new WaitForSeconds(durationInSeconds);
        Hide();
    }
    private void Show()
    {
        gameObject.SetActive(true); 
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }


}
