using Hero;
using UnityEngine;

namespace Bonuses
{
  public class BonusMoneyUseStrategy : BonusUseStrategy
  {
    public override void Pickup(Collider other, int value) => 
      other.GetComponent<HeroMoney>().AddMoney(value);

    public override bool IsCanBePickedUp(Collider other) => 
      other.TryGetComponent(out HeroStateMachine hero);
  }
}