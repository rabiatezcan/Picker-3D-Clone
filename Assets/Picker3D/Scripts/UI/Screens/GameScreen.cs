using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : Screen
{
    [SerializeField] private Picker _picker;
    [SerializeField] private List<Image> _checkPointIndicators;
    [SerializeField] private Color _fillColor;
    [SerializeField] Text _currentLevelText;
    [SerializeField] Text _nextLevelText;

    private int _currentIndex; 
    public override void Show()
    {
        base.Show();
        _picker.OnPassCheckPoint += IncreaseCurrentIndex;
        SetTextsOnStartGame();
        _currentIndex = 0;
        _checkPointIndicators.ForEach(indicator => indicator.color = Color.white);
    }
    public override void Hide()
    {
        _picker.OnPassCheckPoint -= IncreaseCurrentIndex;

        base.Hide();
    }
    private void IncreaseCurrentIndex()
    {
        SetIndicatorColor();
        _currentIndex++;
    }
    private void SetIndicatorColor()
    {
        _checkPointIndicators[_currentIndex].color = _fillColor;
    }
    private void SetTextsOnStartGame()
    {
        _currentLevelText.text = PlayerHelper.Instance.Player.Level.ToString(); 
        _nextLevelText.text = (PlayerHelper.Instance.Player.Level + 1).ToString(); 
    }

}
