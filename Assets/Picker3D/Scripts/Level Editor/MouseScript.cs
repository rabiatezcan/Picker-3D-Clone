using UnityEngine;
using UnityEngine.EventSystems;

public class MouseScript : MonoBehaviour
{
    public enum LevelManipulation { Create, Info, Destroy }; 

    [HideInInspector] 
    public GameEnums.LevelObjects objectType = GameEnums.LevelObjects.Platform; 
    [HideInInspector]
    public LevelManipulation manipulateOption = LevelManipulation.Info; 
    [SerializeField]
    public MeshRenderer mr;
    [HideInInspector]
    public GameObject rotObject;
    [SerializeField] public Camera cam;
    [SerializeField] public Canvas canvas;
    public LevelGenerator ms;

    private Vector3 mousePos;
    private bool colliding;
    private Ray ray;
    private RaycastHit hit;
    private Transform hitTransform;

   
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            colliding = false;
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            ray = cam.ScreenPointToRay(Input.mousePosition); 
            if (Physics.Raycast(ray, out hit))
            {
                var obj = CheckLevelObj(hit.collider);
                if (obj != null)
                {
                    colliding = true; 
                    hitTransform = obj.transform;
                }
            }
            if (!EventSystem.current.IsPointerOverGameObject()) 
            {
                if (manipulateOption == LevelManipulation.Create)
                    CreateObject();
                else if (colliding == true && manipulateOption == LevelManipulation.Info) 
                    SetObjectTransformValues(hitTransform);
            }
        }
    }
    public void Start()
    {
        manipulateOption = LevelManipulation.Info;
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
        if (objectType == GameEnums.LevelObjects.Platform)
        {
            CreateLevelObject(GameEnums.LevelObjects.Platform);
        }
        else if (objectType == GameEnums.LevelObjects.CheckPoint) 
        {
            CreateLevelObject(GameEnums.LevelObjects.CheckPoint);
        }
        else if (objectType == GameEnums.LevelObjects.Ball) 
        {
            CreateLevelObject(GameEnums.LevelObjects.Ball);
        }
        else if (objectType == GameEnums.LevelObjects.WingTrigger) 
        {
            CreateLevelObject(GameEnums.LevelObjects.WingTrigger);
        }
        else if (objectType == GameEnums.LevelObjects.LevelEnd) 
        {
            CreateLevelObject(GameEnums.LevelObjects.LevelEnd);
        }

        manipulateOption = LevelManipulation.Info;
    }

    private void CreateLevelObject(GameEnums.LevelObjects levelObject)
    {
        var newObj = PoolHandler.Instance.POOLS.GetItem(levelObject.ToString());
        newObj.SetPosition(new Vector3(mousePos.x, -.09f, mousePos.z));
        newObj.SetActive();

        EditorObject eo = newObj.gameObject.AddComponent<EditorObject>();
        eo.data.position = newObj.transform.position;
        eo.data.rotation = newObj.transform.rotation;
        eo.data.objectType = levelObject;
        ms.AddLevelObject(eo);
    }

   
    void SetObjectTransformValues(Transform transform)
    {
        ms.OpenInfoPanel(transform);
    }
}
