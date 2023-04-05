using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level Settings")]
public class LevelSettingsScriptableObject : ScriptableObject
{
    public uint LevelNumber;

    [Header("Player settings")]
    public Vector3 PlayerStartPosition;
    public Quaternion PlayerStartRotation;
    public uint PlayerStartLivesCount;
}
