using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Collections;

[CustomEditor(typeof(ShootingController))]
public class ShootingInputControllerInspector : Editor
{
    private SerializedProperty _bulletTypeName;
    private SerializedProperty _selectedTypeIndex;
    private Type[] _allBulletTypes;
    private GUIContent[] _allBulletTypesGUI;

    private void OnEnable()
    {
        _bulletTypeName = serializedObject.FindProperty("bulletTypeName");
        _selectedTypeIndex = serializedObject.FindProperty("selectedTypeIndex");

        _allBulletTypes = (
            from assembly in AppDomain.CurrentDomain.GetAssemblies()
            from type in assembly.GetTypes()
            where type.IsSubclassOf(typeof(BaseBulletMovementController))
                    && !type.IsAbstract select type).ToArray();
        _allBulletTypesGUI = new GUIContent[_allBulletTypes.Count()];

        for (int i = 0; i < _allBulletTypes.Length; i++)
        {
            _allBulletTypesGUI[i] = new GUIContent()
            {
                text = System.Text.RegularExpressions.Regex.Replace(
                _allBulletTypes[i].Name.Replace("MovementController", ""), "([A-Z])", " $1").Trim(),
                tooltip = _allBulletTypes[i].Name
            };
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        _selectedTypeIndex.intValue = EditorGUILayout.Popup(new GUIContent("Bullet type"), _selectedTypeIndex.intValue, _allBulletTypesGUI);
        _bulletTypeName.stringValue = _allBulletTypesGUI[_selectedTypeIndex.intValue].tooltip;
        serializedObject.ApplyModifiedProperties();
    }
}
