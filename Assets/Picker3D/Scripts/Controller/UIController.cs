using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : Controller
{
    [SerializeField] List<Screen> _screens;

    #region States
    public override void Initialize()
    {
        ShowMainScreen();
       
    } 
    public override void StartGame()
    {
        ShowGameScreen();
    }
    public override void Reload()
    {
        ShowMainScreen();
    }
    public override void GameFail()
    {
        ShowLoseScreen();
    }

    public override void GameSuccess()
    {
        ShowWinScreen();
    }
    #endregion

    public void HideAll()
    {
        _screens.ForEach(screen => screen.Hide());
    }
    public void ShowMainScreen()
    {
        HideAll();
        _screens[0].Show();
    } 
    public void ShowGameScreen()
    {
        HideAll();
        _screens[1].Show();
    }    
    public void ShowWinScreen()
    {
        HideAll();
        _screens[2].Show();
        _screens[2].GetComponent<EndScreen>().ShowWinPanel();
    }      
    public void ShowLoseScreen()
    {
        HideAll();
        _screens[2].Show();
        _screens[2].GetComponent<EndScreen>().ShowLosePanel();
    }
}
