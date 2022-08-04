using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : PoolObject
{
    [SerializeField] private List<LevelEndScoreText> _scoreTexts;
    [SerializeField] private PlatformGround _ground;

    private bool _raiseGround;
    public void ScoreAreaTriggerBehaviour()
    {
        _raiseGround = true;
    }
    public override void SetActive()
    {
        Initialize();
        base.SetActive();
    }
    public override void Dismiss()
    {
        _ground.SetDefault();
        _raiseGround = false;
        base.Dismiss();
    }
    private void Initialize()
    {
        for (int i = 0; i < _scoreTexts.Count; i++)
            _scoreTexts[i].InitializeScoreTxtAmount(i+1);

        _ground.Initialize();
    }

    private void Update()
    {
        if (_raiseGround && !_ground.IsRaisingCompleted)
        {
            _ground.RaiseGround();
        }
    }
}
