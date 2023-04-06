using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PlayerObject
{
    [SerializeField]
    private Vector3 objectStartPosition;
    public Vector3 ObjectStartPosition => objectStartPosition;
    [SerializeField]
    private Quaternion objectStartRotation;
    public Quaternion ObjectStartRotation => objectStartRotation;
    [SerializeField]
    GameObject objectPrefab;
    public GameObject ObjectPrefab => objectPrefab;
}

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level Settings")]
public class LevelSettingsScriptableObject : ScriptableObject
{
    [SerializeField]
    private uint levelNumber;
    public uint LevelNumber => levelNumber;

    [Header("Player settings")]
    [SerializeField]
    private uint playerStartLivesCount;
    public uint PlayerStartLivesCount => playerStartLivesCount;
    [SerializeField]
    private PlayerObject mainPlayerObject;
    public PlayerObject MainPlayerObject => mainPlayerObject;
    [SerializeField]
    private List<PlayerObject> playerObjects;
    public List<PlayerObject> PlayerObjects => playerObjects;

    [Header("Asteroids settings")]
    [SerializeField]
    private SerializableTuple<float, float> asteroidsReleasingFrequency;
    public SerializableTuple<float, float> AsteroidsReleasingFrequency => asteroidsReleasingFrequency;
}
