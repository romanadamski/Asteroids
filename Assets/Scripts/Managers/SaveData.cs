using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public List<uint> Highscore;

    public SaveData()
    {
        Highscore = new List<uint>();
    }
}
