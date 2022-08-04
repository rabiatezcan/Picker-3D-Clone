using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelEndScoreText : MonoBehaviour
{
    [SerializeField] private TextMeshPro _scoreAmountTxt;

    private int _scoreAmount;

    public int ScoreAmount => _scoreAmount;

    public void InitializeScoreTxtAmount(int amount)
    {
        _scoreAmount = amount * CONSTANTS.LEVEL_END_SCORE_MULTIPLIER;
        UpdateMultiplierAmountTxt();
    }

    private void UpdateMultiplierAmountTxt()
    {
        _scoreAmountTxt.text = "$" + _scoreAmount;
    }

}
