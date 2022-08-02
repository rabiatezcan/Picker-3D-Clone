using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] private List<LevelEndScoreText> _scoreTexts;

    public void Initialize()
    {
        for (int i = 0; i < _scoreTexts.Count; i++)
            _scoreTexts[i].InitializeScoreTxtAmount(i+1);
    }
}
