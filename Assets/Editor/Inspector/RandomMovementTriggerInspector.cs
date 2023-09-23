using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RandomMovementTrigger)), CanEditMultipleObjects]
public class RandomMovementTriggerInspector : Editor
{
    public SerializedProperty triggerType;
    public SerializedProperty changeMovementFrequency;

    private void OnEnable()
    {
        triggerType = serializedObject.FindProperty("triggerType");
        changeMovementFrequency = serializedObject.FindProperty("changeMovementFrequency");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var triggerTypeValue = (TriggerType)triggerType.enumValueIndex;

        switch (triggerTypeValue)
        {
            case TriggerType.Continuous:
                EditorGUILayout.PropertyField(triggerType);
                EditorGUILayout.PropertyField(changeMovementFrequency);
                break;
            case TriggerType.Single:
                EditorGUILayout.PropertyField(triggerType);
                break;
            default:
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
