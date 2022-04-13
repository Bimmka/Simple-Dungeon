using System;
using StateMachines.Player.Attack;

namespace StaticData.Hero.Attacks.Combo
{
  [Serializable]
  public struct ComboStep
  {
    public AttackType PreviousAttack;
    public AttackType NextAttack;
    public float Delay;
    public float LifeTime;
  }
}