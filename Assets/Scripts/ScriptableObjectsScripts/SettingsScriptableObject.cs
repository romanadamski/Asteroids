using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SettingsScriptableObject", menuName = "ScriptableObjects/Settings")]
public class SettingsScriptableObject : ScriptableObject
{
    [Header("Player settings")]
    public float PlayerMovementSpeed;
    public float PlayerRotationSpeed;
    public float PlayerMovementPrecision;
    public uint PlayerStartLivesCount;

    [Header("Bullet settings")]
    public float BaseBulletMovementSpeed;
    
    [Header("Bullet settings")]
    public float BaseAsteroidMovementSpeed;
}
