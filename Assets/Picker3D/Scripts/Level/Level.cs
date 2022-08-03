using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Level : MonoBehaviour
{
    private LevelEditor _levelEditor;
    [SerializeField] private List<PoolObject> _levelObjects;
    [SerializeField] private Transform _startTransform;
    [SerializeField] private Transform _finishTransform;

    public Vector3 StartPosition => _startTransform.position;
    public Vector3 FinishPosition => _finishTransform.position;

    public void Initialize()
    {
        _levelEditor = new LevelEditor();
        _levelObjects = new List<PoolObject>();
    }

    public void Build()
    {
        string folder = Application.persistentDataPath + "/LevelData/";
        var levelFile = "Level" + PlayerHelper.Instance.Player.Level + ".json";
        Debug.Log(levelFile);

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

        newObj.SetPosition(editorObject.position);
        newObj.SetRotation(editorObject.rotation);
        newObj.SetActive();
        newObj.Initialize();
        _levelObjects.Add(newObj);

    }

    public void Remove()
    {
        _levelObjects.ForEach(obj => obj.Dismiss());
    }

}
