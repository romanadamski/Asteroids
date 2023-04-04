using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EventsManager : BaseManager<EventsManager>
{
    public event Action<uint> PlayerDeath;
    public void OnPlayerLoseLife(uint livesCount)
    {
        PlayerDeath?.Invoke(livesCount);
    }
    
    public event Action<uint> AsteroidShotted;
    public void OnAsteroidShotted(uint livesCount)
    {
        AsteroidShotted?.Invoke(livesCount);
    }

}
