using StaticData.Enemies;
using UnityEngine;

namespace StaticData.Hero.Components
{
  [CreateAssetMenu(fileName = "HeroAttackStaticData", menuName = "Static Data/Hero/Create Hero Attack Data", order = 55)]
  public class HeroAttackStaticData : EnemyAttackStaticData
  {
    public float AgilityCost;
  }
}