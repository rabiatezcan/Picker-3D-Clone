using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : Panel
{
    [SerializeField] private Text _scoreTxt; 
    public override void Hide()
    {
        gameObject.SetActive(false);
    }

    public override void Show()
    {
        gameObject.SetActive(true);
        SetText();
    }

    private void SetText()
    {
        _scoreTxt.text = "Score : " + ScoreSystem.GetCurrentScore();
    }
}
