using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject _creationButtons;
    [SerializeField] GameObject _infoPanel;
    [SerializeField] GameObject _checkPointTargetBallPanel;

    [SerializeField] InputField _checkPointTargetBall;

    [SerializeField] List<EditorObject> _levelObjects = new List<EditorObject>();
    [SerializeField] private MouseScript _mouse;
    [SerializeField] private List<InputField> _posInputs; 
    [SerializeField] private List<InputField> _rotInputs;
    private LevelEditor _levelEditor = new LevelEditor();
    private string _levelName;

    private Transform _currentObj;
    public void SetLevelName(string newLevelName)
    {
        _levelName = newLevelName;
    }
    #region SetInfo
    public void OpenInfoPanel(Transform objTransform)
    {
        _infoPanel.SetActive(true);
        if(objTransform.GetComponentInParent<CheckPoint>() != null)
        {
            _checkPointTargetBallPanel.gameObject.SetActive(true);
            _checkPointTargetBall.text = objTransform.GetComponentInParent<EditorObject>().data.checkPointTargetBallValue.ToString();
        }
        else
        {
            _checkPointTargetBallPanel.gameObject.SetActive(false);
        }
        _currentObj = objTransform;
        _posInputs[0].text = objTransform.position.x.ToString();
        _posInputs[1].text = objTransform.position.y.ToString();
        _posInputs[2].text = objTransform.position.z.ToString();
        _rotInputs[0].text = objTransform.rotation.x.ToString();
        _rotInputs[1].text = objTransform.rotation.y.ToString();
        _rotInputs[2].text = objTransform.rotation.z.ToString();
    }

    public void CloseInfoPanel()
    {
        _infoPanel.SetActive(false);
        _currentObj = null;
    }

    public void SetXAxisPos(string newValue)
    {
        _currentObj.transform.position = new Vector3(float.Parse(newValue), _currentObj.transform.position.y, _currentObj.transform.position.z);
        UpdateObjPositionData();
    }
    public void SetYAxisPos(string newValue)
    {
        _currentObj.transform.position = new Vector3( _currentObj.transform.position.x, float.Parse(newValue), _currentObj.transform.position.z);
        UpdateObjPositionData();
    }    
    public void SetZAxisPos(string newValue)
    {
        _currentObj.transform.position = new Vector3( _currentObj.transform.position.x, _currentObj.transform.position.y, float.Parse(newValue));
        UpdateObjPositionData();
    }   
    public void SetXAxisRot(string newValue)
    {
        _currentObj.transform.rotation = Quaternion.Euler(float.Parse(newValue), _currentObj.transform.rotation.y, _currentObj.transform.rotation.z);
        UpdateObjRotationData();
    }
    public void SetYAxisRot(string newValue)
    {
        _currentObj.transform.rotation = Quaternion.Euler( _currentObj.transform.rotation.x, float.Parse(newValue), _currentObj.transform.rotation.z);
        UpdateObjRotationData();
    }    
    public void SetZAxisRot(string newValue)
    {
        _currentObj.transform.rotation = Quaternion.Euler(_currentObj.transform.rotation.x, _currentObj.transform.rotation.y, float.Parse(newValue));
        UpdateObjRotationData();
    }

    public void UpdateObjPositionData()
    {
        _currentObj.GetComponent<EditorObject>().data.position = _currentObj.transform.position;
    } 
    public void UpdateObjRotationData()
    {
        _currentObj.GetComponent<EditorObject>().data.rotation = _currentObj.transform.rotation;
    }
    public void DeleteObj()
    {
        _levelObjects.Remove(_currentObj.GetComponentInParent<EditorObject>());
        Destroy(_currentObj.gameObject);
        CloseInfoPanel();
    }

    public void SetTargetBallValue(string newValue)
    {
        _currentObj.GetComponentInParent<CheckPoint>().TargetBallValue = int.Parse(newValue);
        _currentObj.GetComponent<EditorObject>().data.checkPointTargetBallValue = int.Parse(newValue);
    }
    #endregion
    #region Creations

    public void AddLevelObject(EditorObject obj)
    {
        _levelObjects.Add(obj);
    }
    public void OpenCreationButtons()
    {
        _creationButtons.SetActive(true);
        _mouse.manipulateOption = MouseScript.LevelManipulation.Create;

    }

    public void CloseCreationButtons()
    {
        _creationButtons.SetActive(false);
    }

    public void CreatePlatform()
    {
        _mouse.objectType = GameEnums.LevelObjects.Platform;
        CloseCreationButtons();
    }
    public void CreateBall()
    {
        _mouse.objectType = GameEnums.LevelObjects.Ball;
        CloseCreationButtons();
    }
    public void CreateWingTrigger()
    {
        _mouse.objectType = GameEnums.LevelObjects.WingTrigger;
        CloseCreationButtons();
    }
    public void CreateCheckpoint()
    {
        _mouse.objectType = GameEnums.LevelObjects.CheckPoint;
        CloseCreationButtons();
    }
    public void CreateLevelEnd()
    {
        _mouse.objectType = GameEnums.LevelObjects.LevelEnd;
        CloseCreationButtons();
    }
    #endregion

    #region Save/Load
    public void SaveLevel()
    {
        string folder = "Assets/Picker3D/LevelData/";
        string levelFile = "";

        if (_levelName == "")
            levelFile = "new_level.json";
        else
            levelFile = _levelName + ".json";

        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        string path = Path.Combine(folder, levelFile);

        if (File.Exists(path))
        {
            _levelEditor.editorObjects.Clear();
            File.Delete(path);
        }
        _levelObjects.ForEach(obj => _levelEditor.editorObjects.Add(obj.data));

        string json = JsonUtility.ToJson(_levelEditor);

        File.WriteAllText(path, json);

        ClearAll();
    }

    private void ClearAll()
    {
        _levelObjects.ForEach(obj => Destroy(obj.gameObject));
        _levelObjects.Clear();
    }
    public void LoadLevel()
    {
        _levelObjects.Clear();

        string folder = "Assets/Picker3D/LevelData/";
        string levelFile = "";

        if (_levelName == "")
            levelFile = "new_level.json";
        else
            levelFile = _levelName + ".json";

        string path = Path.Combine(folder, levelFile);
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

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

    void CreateFromFile()
    {
        for (int i = 0; i < _levelEditor.editorObjects.Count; i++)
        {
            CreateLevelObject(_levelEditor.editorObjects[i]);
        }
    }

    private void CreateLevelObject(EditorObject.Data editorObject)
    {
        var newObj = PoolHandler.Instance.POOLS.GetItem(editorObject.objectType.ToString());
        newObj.SetPosition(editorObject.position);
        newObj.SetRotation(editorObject.rotation);
        newObj.SetActive();

        EditorObject eo = newObj.gameObject.AddComponent<EditorObject>();
        eo.data.position = newObj.transform.position;
        eo.data.rotation = newObj.transform.rotation;
        eo.data.checkPointTargetBallValue = editorObject.checkPointTargetBallValue;
        eo.data.objectType = editorObject.objectType;
        
        _levelObjects.Add(eo);

    }
    #endregion

}
