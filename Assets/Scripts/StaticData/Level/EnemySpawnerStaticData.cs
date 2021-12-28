using System;
using Enemies;
using UnityEngine;

namespace StaticData.Level
{
  [Serializable]
  public class EnemySpawnerStaticData
  {
    public string Id;
    public Vector3 Position;

    public EnemySpawnerStaticData(string id, Vector3 position)
    {
      Id = id;
      Position = position;
    }
  }
}