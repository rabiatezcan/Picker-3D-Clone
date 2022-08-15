using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObjectCreation : MonoBehaviour
{
    [SerializeField] GameObject _creationButtons; 

    private LevelEditorInputHandler _input;
    public void Initialize(LevelEditorInputHandler input)
    {
        _input = input;
    }
    public void OpenCreationButtons()
    {
        _creationButtons.SetActive(true);
        _input.LevelManipulationType = GameEnums.LevelEditorTypes.Create;
    }

    public void CloseCreationButtons()
    {
        _creationButtons.SetActive(false);
    }
    public void CreatePlatform()
    {
        _input.ObjectType = GameEnums.LevelObjects.Platform;
        CloseCreationButtons();
    }
    public void CreateBall()
    {
        _input.ObjectType = GameEnums.LevelObjects.Ball;
        CloseCreationButtons();
    }
    public void CreateWingTrigger()
    {
        _input.ObjectType = GameEnums.LevelObjects.WingTrigger;
        CloseCreationButtons();
    }
    public void CreateCheckpoint()
    {
        _input.ObjectType = GameEnums.LevelObjects.CheckPoint;
        CloseCreationButtons();
    }
    public void CreateLevelEnd()
    {
        _input.ObjectType = GameEnums.LevelObjects.LevelEnd;
        CloseCreationButtons();
    }
}
