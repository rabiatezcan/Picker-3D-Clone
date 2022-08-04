using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private List<PoolObject> _previousLevelObj;
    [SerializeField] private List<PoolObject> _levelObjects;

    private LevelEditor _levelEditor;

    private float _finishZAxisValue;
    private float _startZAxisValue;
    private float _currentOffset;
    private int _currentLevelIndex;
    public float StartZAxisValue => _startZAxisValue;
    public void Initialize()
    {
        _levelEditor = new LevelEditor();
        _levelObjects = new List<PoolObject>();
        _previousLevelObj = new List<PoolObject>();
        _currentOffset = 0;
        _finishZAxisValue = 0f;
        _startZAxisValue = 0f;
    }
    public void Build(int currentLevelIndexOffset)
    {
        string folder = Application.persistentDataPath + "/LevelData/";
         
        if(currentLevelIndexOffset == 1)
            _currentOffset += CONSTANTS.OBJECT_OFFSET_VALUE_PER_LEVEL;

        if (PlayerHelper.Instance.Player.Level >= CONSTANTS.MAX_LEVEL_COUNT)
        {
            _currentLevelIndex = Random.Range(1, CONSTANTS.MAX_LEVEL_COUNT);
        }
        else
        {
            _currentLevelIndex = PlayerHelper.Instance.Player.Level;
        }

        var levelFile = "Level" + (_currentLevelIndex + currentLevelIndexOffset) + ".json";

        string path = Path.Combine(folder, levelFile);

        if (File.Exists(path))
        {
            EditorObject[] foundObjects = FindObjectsOfType<EditorObject>();
            foreach (EditorObject obj in foundObjects)
                Destroy(obj.gameObject);

            string json = File.ReadAllText(path);
            _levelEditor = JsonUtility.FromJson<LevelEditor>(json);
            CreateFromFile();
        }
        else
        {
            Debug.LogWarning("There is no file");
        }
        _finishZAxisValue = _levelObjects[_levelObjects.Count - 1].transform.position.z;
        _startZAxisValue = _levelObjects[0].transform.position.z;
    }

    private void CreateFromFile()
    {
        for (int i = 0; i < _levelEditor.editorObjects.Count; i++)
        {
            CreateLevelObject(_levelEditor.editorObjects[i]);
        }

        
    }

    private void CreateLevelObject(EditorObject.Data editorObject)
    {
        var newObj = PoolHandler.Instance.POOLS.GetItem(editorObject.objectType.ToString());

        if(editorObject.objectType == GameEnums.LevelObjects.CheckPoint)
        {
            newObj.GetComponent<CheckPoint>().TargetBallValue = editorObject.checkPointTargetBallValue;
        }

        newObj.SetPosition(editorObject.position + (Vector3.forward * _currentOffset));
        newObj.SetRotation(editorObject.rotation);
        newObj.SetActive();
        _levelObjects.Add(newObj);

    }

    public void Remove()
    {
        _levelObjects.ForEach(obj => obj.Dismiss());
        _levelObjects.Clear();
    }

}
