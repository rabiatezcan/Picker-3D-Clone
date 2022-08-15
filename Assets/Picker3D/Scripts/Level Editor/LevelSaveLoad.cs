using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelSaveLoad : MonoBehaviour
{
    private List<LevelObject> _levelObjects = new List<LevelObject>();
    private LevelEditor _levelEditor = new LevelEditor();
    private string _levelName;

    public void AddLevelObject(LevelObject obj)
    {
        _levelObjects.Add(obj);
    }
    public void RemoveLevelObject(LevelObject obj)
    {
        _levelObjects.Remove(obj);
    }

    public void SetLevelName(string newLevelName)
    {
        _levelName = newLevelName;
    }

    public void SaveLevel()
    {
        var levelFile = "";

        if (_levelName == "")
            levelFile = "new_level.json";
        else
            levelFile = _levelName + ".json";

        if (!Directory.Exists(CONSTANTS.LEVEL_DATA_FOLDER_PATH))
            Directory.CreateDirectory(CONSTANTS.LEVEL_DATA_FOLDER_PATH);

        var path = Path.Combine(CONSTANTS.LEVEL_DATA_FOLDER_PATH, levelFile);

        if (File.Exists(path))
        {
            _levelEditor.editorObjects.Clear();
            File.Delete(path);
        }
        _levelObjects.ForEach(obj => _levelEditor.editorObjects.Add(obj.data));

        var json = JsonUtility.ToJson(_levelEditor);

        File.WriteAllText(path, json);

        ClearAll();


    }
    public void LoadLevel()
    {
        _levelObjects.Clear();

        var levelFile = "";

        if (_levelName == "")
            levelFile = "new_level.json";
        else
            levelFile = _levelName + ".json";

        var path = Path.Combine(CONSTANTS.LEVEL_DATA_FOLDER_PATH, levelFile);

        if (!Directory.Exists(CONSTANTS.LEVEL_DATA_FOLDER_PATH))
            Directory.CreateDirectory(CONSTANTS.LEVEL_DATA_FOLDER_PATH);

        if (File.Exists(path))
        {
            LevelObject[] foundObjects = FindObjectsOfType<LevelObject>();
            foreach (LevelObject obj in foundObjects)
                Destroy(obj.gameObject);

            var json = File.ReadAllText(path);
            _levelEditor = JsonUtility.FromJson<LevelEditor>(json);
            CreateFromFile();
        }
        else
        {
            Debug.LogWarning("There is no file");
        }
    }

    private void CreateFromFile()
    {
        for (int i = 0; i < _levelEditor.editorObjects.Count; i++)
        {
            CreateLevelObject(_levelEditor.editorObjects[i]);
        }
    }

    private void CreateLevelObject(LevelObject.Data editorObject)
    {
        var newObj = PoolHandler.Instance.POOLS.GetItem(editorObject.objectType.ToString());
        newObj.SetPosition(editorObject.position);
        newObj.SetRotation(editorObject.rotation);
        newObj.SetActive();

        var levelOnj = newObj.gameObject.AddComponent<LevelObject>();
        levelOnj.data.position = newObj.transform.position;
        levelOnj.data.rotation = newObj.transform.rotation;
        levelOnj.data.checkPointTargetBallValue = editorObject.checkPointTargetBallValue;
        levelOnj.data.objectType = editorObject.objectType;

        AddLevelObject(levelOnj);

    }

    private void ClearAll()
    {
        _levelObjects.ForEach(obj => Destroy(obj.gameObject));
        _levelObjects.Clear();
    }
}
