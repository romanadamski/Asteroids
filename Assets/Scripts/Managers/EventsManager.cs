using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    public event Action<string> PlayerDeath;
    public void OnPlayerDeath(string name)
    {
        PlayerDeath?.Invoke(name);
    }

}
