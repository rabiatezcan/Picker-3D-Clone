using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnums
{ 
    public enum LevelObjects
    {
        Platform, 
        CheckPoint,
        Ball, 
        WingTrigger, 
        LevelEnd
    }
    public enum GameState
    {
        Loading, 
        Main,
        Game,
        End
    }

    public enum LevelEditorTypes
    { 
        Create, 
        Info, 
        Destroy 
    };


    public enum CheckPointStates
    {
        Idle,
        Counting,
        PlatformRaise,
        Barrier,

    }
    public enum ObjectType
    {

    }  

    
}
