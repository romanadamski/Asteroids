using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ShipObject
{
    [SerializeField]
    private Vector2 objectStartPosition;
    public Vector2 ObjectStartPosition => objectStartPosition;

    [SerializeField]
    private Quaternion objectStartRotation;
    public Quaternion ObjectStartRotation => objectStartRotation;

    [SerializeField]
    GameObject objectPrefab;
    public GameObject ObjectPrefab => objectPrefab;
}

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level Settings")]
public class LevelSettingsSO : ScriptableObject
{
    [SerializeField]
    private uint levelNumber;
    public uint LevelNumber => levelNumber;
    [SerializeField]
    private Sprite backgroundImage;
    public Sprite BackgroundSprite => backgroundImage;

    [Header("Player settings")]
    [SerializeField]
    private bool activatePlayer = true;
    public bool ActivatePlayer => activatePlayer;

    [SerializeField]
    private ShipObject mainPlayerObject;
    public ShipObject MainPlayerObject => mainPlayerObject;

    [SerializeField]
    private List<ShipObject> playerObjects;
    public List<ShipObject> PlayerObjects => playerObjects;

    [Header("Asteroids settings")]
    [SerializeField]
    private SerializableTuple<float, float> asteroidsReleasingFrequency;
    public SerializableTuple<float, float> AsteroidsReleasingFrequency => asteroidsReleasingFrequency;

    [SerializeField]
    private SerializableTuple<float, float> asteroidsSpeedRange;
    public SerializableTuple<float, float> AsteroidsSpeedRange => asteroidsSpeedRange;

    [Header("Enemy settings")]
    [SerializeField]
    private List<ShipObject> enemyObjects;
    public List<ShipObject> EnemyObjects => enemyObjects;
}
