using System;
using StateMachines.Player.Attack;

namespace StaticData.Hero.Attacks
{
  [Serializable]
  public struct AttackStaticData
  {
    public AttackType Attack;
    public int Cost;
    public int Damage;
    public float AttackCooldown;
    public float AttackRadius;
  }
}