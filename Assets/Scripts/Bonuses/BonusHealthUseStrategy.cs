using Systems.Healths;
using Hero;
using UnityEngine;

namespace Bonuses
{
  public class BonusHealthUseStrategy : BonusUseStrategy
  {
    public override void Pickup(Collider other, int value) => 
      other.GetComponentInChildren<IHealth>().AddHealth(value);

    public override bool IsCanBePickedUp(Collider other) => 
      other.TryGetComponent(out HeroStateMachine hero);
  }
}