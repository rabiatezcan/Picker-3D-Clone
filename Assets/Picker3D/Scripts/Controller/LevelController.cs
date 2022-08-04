using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : Controller
{
    private Level _currentLevel = new Level();
    [SerializeField] private Picker _picker;

    #region States
    public override void Initialize()
    {
        LoadLevel();
    }
    public override void StartGame()
    {
        _picker.PickerPhysic.OnRampStart += LoadLevelOnGameplay;

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
        UnloadLevel();
    }
    #endregion

    private void LoadLevelOnGameplay()
    {
        _currentLevel.Initialize();
        _currentLevel.Build(1);
        _picker.StartPos = Vector3.forward * _currentLevel.StartZAxisValue;
    }
    private void LoadLevel()
    {
        _currentLevel.Initialize();
        _currentLevel.Build(0);
        _picker.StartPos = Vector3.forward * _currentLevel.StartZAxisValue;
    }


    private void UnloadLevel()
    {
        _currentLevel.Remove();
    }
}
