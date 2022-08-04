using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : Controller
{
    private Level _currentLevel;
    [SerializeField] private Picker _picker;

    private bool _islevelLoad;

    #region States
    public override void Initialize()
    {
        _currentLevel = new Level();
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
        _islevelLoad = false;
    }
    public override void GameFail()
    {

    }

    public override void GameSuccess()
    {
    }
    #endregion

    private void LoadLevelOnGameplay()
    {
        if (!_islevelLoad)
        {
            _currentLevel.Initialize();
            _currentLevel.Build(1);
            _picker.StartPos = Vector3.forward * _currentLevel.StartZAxisValue;
            _islevelLoad = true;
        }

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
