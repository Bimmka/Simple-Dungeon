using System;
using Enemies.Spawn;

namespace StaticData.Level
{
  [Serializable]
  public struct WaveData
  {
    public int WaveIndex;
    public EnemyTypeId[] Enemies;
    public float WaveWaitTime;
  }
}