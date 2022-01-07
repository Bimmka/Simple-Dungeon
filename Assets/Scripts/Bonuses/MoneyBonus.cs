using Hero;
using UnityEngine;

namespace Bonuses
{
  public class MoneyBonus : Bonus
  {
    protected override void Pickup(Collider other, int value) => 
      other.GetComponent<HeroMoney>().AddMoney(value);

    protected override bool IsCanBePickedUp(Collider other) => 
      other.TryGetComponent(out HeroStateMachine hero);
  }
}