using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : Controller
{
    private Level _currentLevel = new Level();

    #region States
    public override void Initialize(GameManager gameManager)
    {
        _currentLevel.Initialize();
        LoadLevel();
    }
    public override void StartGame()
    {
    }
    public override void Reload()
    {
        UnloadLevel();
        LoadLevel();
    }
    public override void GameFail()
    {
    }

    public override void GameSuccess()
    {
    }
    #endregion

    private void LoadLevel()
    {
        _currentLevel.Build();
    }

    private void UnloadLevel()
    {
        _currentLevel.Remove();
        Destroy(_currentLevel.gameObject);
        _currentLevel = null;
    }
}
