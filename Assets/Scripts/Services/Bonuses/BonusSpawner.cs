using System;
using System.Collections.Generic;
using Bonuses;
using Enemies.Spawn;
using UnityEngine;

namespace Services.Bonuses
{
  public class BonusSpawner : IBonusSpawner
  {
    private readonly List<SpawnPoint> spawnPoints = new List<SpawnPoint>(20);

    public event Action<GameObject> Spawned;
    
    public void AddPoint(SpawnPoint spawnPoint) => 
      spawnPoints.Add(spawnPoint);

    public void SpawnBonus(BonusTypeId typeId)
    {
      
    }
  }
}