using System;
using Enemies.Spawn;
using UnityEngine;

namespace StaticData.Loot
{
  [Serializable]
  public struct EnemyLoot
  {
    public EnemyTypeId[] TypeIds;
    public Vector2Int MoneyCountRange;
  }
}