using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Picker3D/Level Serialization", order = 1)]

public class LevelSerialization : ScriptableObject
{
    [SerializeField] List<Level> levels;

    public Level this[int i] => levels[i];
    public int Count => levels.Count;
}
