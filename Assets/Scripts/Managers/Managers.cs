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
    public static InputManager InputManager;
    public static EventsManager EventsManager;
    public static ObjectPoolingManager ObjectPoolingManager;
    public static StatesManager StatesManager;
    public static ScreenManager ScreenManager;

    private void Awake()
    {
        foreach (var field in GetType().GetFields())
        {
            field.SetValue(this, FindObjectOfType(field.FieldType));
        }
    }
}
