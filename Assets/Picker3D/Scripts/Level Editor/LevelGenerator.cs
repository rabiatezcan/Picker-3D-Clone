using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager; 
    [SerializeField] private LevelObjectCreation _levelObjectCreation;
    [SerializeField] private LevelObjectInfo _levelObjectInfo;
    [SerializeField] private LevelSaveLoad _levelSaveLoader;

    [SerializeField] private LevelEditorInputHandler _inputHandler;

    private void Awake()
    {
        if (gameObject.activeInHierarchy)
            _gameManager.gameObject.SetActive(false);

        _inputHandler.Initialize();
        _levelObjectCreation.Initialize(_inputHandler);
        _inputHandler.OnObjectSelected += OpenInfoPanel;
        _inputHandler.OnNewObjectCreated += AddLevelObject;
        _levelObjectInfo.OnObjectRemoved += RemoveLevelObject;
    }

    public void OpenInfoPanel(Transform objTransform)
    {
        _levelObjectInfo.OpenInfoPanel(objTransform);
    }

    public void AddLevelObject(LevelObject obj)
    {
        _levelSaveLoader.AddLevelObject(obj);
    }  
    public void RemoveLevelObject(LevelObject obj)
    {
        _levelSaveLoader.RemoveLevelObject(obj);
    }

}
