using UnityEngine;
using System;

public class LevelObject : MonoBehaviour
{
    [Serializable]
    public struct Data
    {
        public Vector3 position;
        public Quaternion rotation; 
        public GameEnums.LevelObjects objectType;
        public int checkPointTargetBallValue;
    }

    public Data data; 
}
