using Bonuses;
using UnityEngine;

namespace Services.Bonuses
{
  public interface IBonusFactory : ICleanupService
  {
    Bonus SpawnBonus(BonusTypeId typeId, Transform parent);
  }
}