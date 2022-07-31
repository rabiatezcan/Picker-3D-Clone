using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : Screen
{
    [SerializeField] private List<Panel> _panels; 

    public void ShowWinPanel()
    {
        HideAll();
        _panels[0].Show();
    }

    public void ShowLosePanel()
    {
        HideAll();
        _panels[1].Show();
    }

    public void HideAll()
    {
        _panels.ForEach(panel => panel.Hide());
    }
}
