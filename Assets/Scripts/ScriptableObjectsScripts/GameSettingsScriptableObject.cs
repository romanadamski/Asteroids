using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/Game Settings")]
public class GameSettingsScriptableObject : ScriptableObject
{
    [Header("Player settings")]
    [SerializeField]
    public float playerMovementSpeed;
    public float PlayerMovementSpeed => playerMovementSpeed;
    [SerializeField]
    public float playerRotationSpeed;
    public float PlayerRotationSpeed => playerRotationSpeed;
    [SerializeField]
    public float playerMovementPrecision;
    public float PlayerMovementPrecision => playerMovementPrecision;

    [Header("Bullet settings")]
    [SerializeField]
    public float baseBulletMovementSpeed;
    public float BaseBulletMovementSpeed => baseBulletMovementSpeed;

    [Header("Asteroid settings")]
    [SerializeField]
    public float baseAsteroidMovementSpeed;
    public float BaseAsteroidMovementSpeed => baseAsteroidMovementSpeed;
}
