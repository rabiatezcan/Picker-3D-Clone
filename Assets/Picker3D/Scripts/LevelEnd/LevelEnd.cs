using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : PoolObject
{
    [SerializeField] private List<LevelEndScoreText> _scoreTexts;

    public override void Initialize()
    {
        base.Initialize();
        for (int i = 0; i < _scoreTexts.Count; i++)
            _scoreTexts[i].InitializeScoreTxtAmount(i+1);
    }
}
