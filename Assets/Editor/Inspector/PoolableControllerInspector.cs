using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BasePoolableController), true)]
public class PoolableControllerInspector : Editor
{
    SerializedProperty _poolableType;
    private SerializedProperty _selectedTypeIndex;
    private string[] _poolableTypes;
    private GUIContent[] _poolableTypesGUI;

    private void OnEnable()
    {
        _poolableType = serializedObject.FindProperty("PoolableType");
        _selectedTypeIndex = serializedObject.FindProperty("selectedTypeIndex");

        _poolableTypes = serializedObject.targetObject.GetType().GetProperty("PoolableTypes").GetValue(serializedObject.targetObject) as string[];
        _poolableTypesGUI = new GUIContent[_poolableTypes.Count()];

        for (int i = 0; i < _poolableTypes.Length; i++)
        {
            _poolableTypesGUI[i] = new GUIContent()
            {
                text = System.Text.RegularExpressions.Regex.Replace(
                _poolableTypes[i].Replace("MovementController", ""), "([A-Z])", " $1").Trim(),
                tooltip = _poolableTypes[i]
            };
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        _selectedTypeIndex.intValue = EditorGUILayout.Popup(new GUIContent("Poolable type"), _selectedTypeIndex.intValue, _poolableTypesGUI);
        _poolableType.stringValue = _poolableTypesGUI[_selectedTypeIndex.intValue].tooltip;

        serializedObject.ApplyModifiedProperties();
    }
}
