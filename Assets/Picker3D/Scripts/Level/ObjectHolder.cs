using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHolder : MonoBehaviour
{
    [SerializeField] private GameEnums.ObjectType _objectType;

    private PoolObject _levelObject;

    private void InitializeObject()
    {
        //_levelObject = PoolManager.Pools.GetItem(_objectType.ToString());
        _levelObject.SetActiveWithPosition(transform.position);
        _levelObject.transform.rotation = transform.rotation;
    }

    public void SetActive()
    {
        SetDisactive();
        InitializeObject();
    }

    public void SetDisactive()
    {
        _levelObject?.Dismiss();
        _levelObject = null;
    }

    #region Gizmos
    private void OnDrawGizmos()
    {
        //switch (_objectType)
        //{
        //    case GameEnums.ObjectType.Collectible:
        //        Visual(Color.yellow, new Vector3(1f, 1f, .2f), true, 0f);
        //        break;
        //    case GameEnums.ObjectType.SawShort:
        //        Visual(Color.blue, new Vector3(2f, .1f, .2f), true, 0f);
        //        break;
        //    case GameEnums.ObjectType.SawLong:
        //        Visual(Color.blue, new Vector3(3.5f, .1f, .2f), true, 0f);
        //        break;
        //}
    }
    private void Visual(Color newColor, Vector3 size, bool cube = false, float height = 1.0f, float radius = 1.0f)
    {
        Gizmos.color = newColor;
        if (cube)
        {
            Gizmos.DrawWireCube(transform.position + transform.up * height, size);
        }
        else
        {
            Gizmos.DrawWireSphere(transform.position + transform.up * height, radius);
        }
    }
    #endregion
}
