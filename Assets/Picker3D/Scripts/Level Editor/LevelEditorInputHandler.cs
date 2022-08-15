using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelEditorInputHandler : MonoBehaviour
{
    public Action<Transform> OnObjectSelected;
    public Action<LevelObject> OnNewObjectCreated;

    #region Variables
    [SerializeField] private Camera _cam;

    private GameEnums.LevelObjects _objectType = GameEnums.LevelObjects.Platform; 
    private GameEnums.LevelEditorTypes _levelManipulationType = GameEnums.LevelEditorTypes.Info; 
    private Transform _hitTransform;
    private Vector3 _mousePos;
    private RaycastHit _hit;
    private Ray _ray;
    private bool _colliding;

    #endregion

    public GameEnums.LevelEditorTypes LevelManipulationType 
    {
        get => _levelManipulationType;
        set
        {
            _levelManipulationType = value;
        }
    }

    public GameEnums.LevelObjects ObjectType 
    { 
        get => _objectType; 
        set => _objectType = value; 
    }

    public void Initialize()
    {
        _levelManipulationType = GameEnums.LevelEditorTypes.Info;
    }


    public PoolObject CheckLevelObj(Collider other)
    {
        if(other.tag == GameEnums.LevelObjects.Ball.ToString())
        {
            return other.GetComponentInParent<Ball>();
        } 
        else if(other.tag == GameEnums.LevelObjects.Platform.ToString())
        {
            return other.GetComponentInParent<PoolObject>();
        }   
        else if(other.tag == GameEnums.LevelObjects.CheckPoint.ToString())
        {
            return other.GetComponentInParent<CheckPoint>();
        }
        else if (other.tag == GameEnums.LevelObjects.LevelEnd.ToString())
        {
            return other.GetComponentInParent<LevelEnd>();
        }
        else if (other.tag == GameEnums.LevelObjects.WingTrigger.ToString())
        {
            return other.GetComponentInParent<WingTrigger>();
        }
        else
        {
            return null;
        }
    }

    public void CreateObject()
    {
        if (ObjectType == GameEnums.LevelObjects.Platform)
        {
            CreateLevelObject(GameEnums.LevelObjects.Platform);
        }
        else if (ObjectType == GameEnums.LevelObjects.CheckPoint) 
        {
            CreateLevelObject(GameEnums.LevelObjects.CheckPoint);
        }
        else if (ObjectType == GameEnums.LevelObjects.Ball) 
        {
            CreateLevelObject(GameEnums.LevelObjects.Ball);
        }
        else if (ObjectType == GameEnums.LevelObjects.WingTrigger) 
        {
            CreateLevelObject(GameEnums.LevelObjects.WingTrigger);
        }
        else if (ObjectType == GameEnums.LevelObjects.LevelEnd) 
        {
            CreateLevelObject(GameEnums.LevelObjects.LevelEnd);
        }

        LevelManipulationType = GameEnums.LevelEditorTypes.Info;
    }

    private void CreateLevelObject(GameEnums.LevelObjects levelObject)
    {
        var newObj = PoolHandler.Instance.POOLS.GetItem(levelObject.ToString());
        newObj.SetPosition(new Vector3(_mousePos.x, -.09f, _mousePos.z));
        newObj.SetActive();

        LevelObject eo = newObj.gameObject.AddComponent<LevelObject>();
        eo.data.position = newObj.transform.position;
        eo.data.rotation = newObj.transform.rotation;
        eo.data.objectType = levelObject;
        OnNewObjectCreated?.Invoke(eo);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _colliding = false;
            _mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
            _ray = _cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out _hit))
            {
                var obj = CheckLevelObj(_hit.collider);
                if (obj != null)
                {
                    _colliding = true;
                    _hitTransform = obj.transform;
                }
            }
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (LevelManipulationType == GameEnums.LevelEditorTypes.Create)
                    CreateObject();
                else if (_colliding == true && LevelManipulationType == GameEnums.LevelEditorTypes.Info)
                    OnObjectSelected?.Invoke(_hitTransform);
            }
        }
    }
}
