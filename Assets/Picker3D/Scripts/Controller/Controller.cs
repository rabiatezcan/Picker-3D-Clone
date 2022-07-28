using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    protected GameManager _gameManager;

    public abstract void Initialize(GameManager gameManager);
    public abstract void StartGame();
    
    public abstract void Reload();
    public abstract void GameSuccess();
    public abstract void GameFail();

}
