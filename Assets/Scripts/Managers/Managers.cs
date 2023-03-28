using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public static GameManager GameManager;
    public static SettingsManager SettingsManager;

    private void Awake()
    {
        foreach (var field in GetType().GetFields())
        {
            field.SetValue(this, FindObjectOfType(field.FieldType));
        }
    }
}
