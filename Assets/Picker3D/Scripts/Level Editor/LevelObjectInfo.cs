using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelObjectInfo : MonoBehaviour
{
    public Action<LevelObject> OnObjectRemoved; 
    [SerializeField] GameObject _checkPointTargetBallPanel;
    [SerializeField] InputField _checkPointTargetBall;
    [SerializeField] private List<InputField> _posInputs;
    [SerializeField] private List<InputField> _rotInputs;

    private LevelObject _currentObj;

    public void OpenInfoPanel(Transform objTransform)
    {
        gameObject.SetActive(true);

        _currentObj = objTransform.GetComponent<LevelObject>();

        CheckPointObjControl();
        InitializeTexts(objTransform);
    }

    public void CloseInfoPanel()
    {
        gameObject.SetActive(false);
        _currentObj = null;
    }
    private void InitializeTexts(Transform objTransform)
    {
        _posInputs[0].text = objTransform.position.x.ToString();
        _posInputs[1].text = objTransform.position.y.ToString();
        _posInputs[2].text = objTransform.position.z.ToString();
        _rotInputs[0].text = objTransform.rotation.x.ToString();
        _rotInputs[1].text = objTransform.rotation.y.ToString();
        _rotInputs[2].text = objTransform.rotation.z.ToString();
    }

    #region CheckPoint Obj
    private void CheckPointObjControl()
    {
        if (_currentObj.GetComponent<CheckPoint>() != null)
        {
            _checkPointTargetBallPanel.gameObject.SetActive(true);
            _checkPointTargetBall.text = _currentObj.data.checkPointTargetBallValue.ToString();
        }
        else
        {
            _checkPointTargetBallPanel.gameObject.SetActive(false);
        }
    }
    public void SetTargetBallValue(string newValue)
    {
        _currentObj.transform.GetComponent<CheckPoint>().TargetBallValue = int.Parse(newValue);
        _currentObj.data.checkPointTargetBallValue = int.Parse(newValue);
    }

    #endregion

    #region Set Positions
    public void SetXAxisPos(string newValue)
    {
        _currentObj.transform.position = new Vector3(float.Parse(newValue), _currentObj.transform.position.y, _currentObj.transform.position.z);
        UpdateObjPositionData();
    }
    public void SetYAxisPos(string newValue)
    {
        _currentObj.transform.position = new Vector3(_currentObj.transform.position.x, float.Parse(newValue), _currentObj.transform.position.z);
        UpdateObjPositionData();
    }
    public void SetZAxisPos(string newValue)
    {
        _currentObj.transform.position = new Vector3(_currentObj.transform.position.x, _currentObj.transform.position.y, float.Parse(newValue));
        UpdateObjPositionData();
    }

    public void UpdateObjPositionData()
    {
        _currentObj.data.position = _currentObj.transform.position;
    }
    #endregion

    #region Set Rotations
    public void SetXAxisRot(string newValue)
    {
        _currentObj.transform.rotation = Quaternion.Euler(float.Parse(newValue), _currentObj.transform.rotation.y, _currentObj.transform.rotation.z);
        UpdateObjRotationData();
    }
    public void SetYAxisRot(string newValue)
    {
        _currentObj.transform.rotation = Quaternion.Euler(_currentObj.transform.rotation.x, float.Parse(newValue), _currentObj.transform.rotation.z);
        UpdateObjRotationData();
    }
    public void SetZAxisRot(string newValue)
    {
        _currentObj.transform.rotation = Quaternion.Euler(_currentObj.transform.rotation.x, _currentObj.transform.rotation.y, float.Parse(newValue));
        UpdateObjRotationData();
    }

    public void UpdateObjRotationData()
    {
        _currentObj.data.rotation = _currentObj.transform.rotation;
    }
    #endregion

    

    public void DeleteObj()
    {
        OnObjectRemoved?.Invoke(_currentObj);
        Destroy(_currentObj.gameObject);
        CloseInfoPanel();
    }
}
