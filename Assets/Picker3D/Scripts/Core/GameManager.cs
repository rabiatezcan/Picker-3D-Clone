using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private DataManager _dataManager;
    [SerializeField] private PoolManager _poolManager;

    [Header("Controllers")]
    [SerializeField] private List<Controller> _controllers;

    private static GameManager _gameManager; 
    public static GameManager Instance
    {
        get
        {
            if(_gameManager == null)
            {
                
            }

            return _gameManager;
        }
    }

    #region Init

    private void Awake()
    {
        _gameManager = this;
    }
    private void Start()
    {
        Initialize();
    }
    #endregion

    #region States
    public void Initialize()
    {
        _dataManager.Initialize();
        _poolManager.Initialize();
        _controllers.ForEach(controller => controller.Initialize(this));
    }

    public void StartGame()
    {
        _controllers.ForEach(controller => controller.StartGame());
    }

    public void Reload()
    {
        _controllers.ForEach(controller => controller.Reload());
    }
    public void GameSuccess()
    {
        _controllers.ForEach(controller => controller.GameSuccess());

        GameOver();
    }
    public void GameFail()
    {
        _controllers.ForEach(controller => controller.GameFail());

        GameOver();
    }
    public void GameOver()
    {
    }
    #endregion
}
