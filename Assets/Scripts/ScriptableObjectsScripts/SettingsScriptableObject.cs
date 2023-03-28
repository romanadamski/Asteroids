using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SettingsScriptableObject", menuName = "ScriptableObjects/Settings")]
public class SettingsScriptableObject : ScriptableObject
{
    public float PlayerMovementSpeed;
    public float PlayerRotationSpeed;
    public float PlayerMovementPrecision;
}
