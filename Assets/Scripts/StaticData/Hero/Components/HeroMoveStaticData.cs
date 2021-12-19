using UnityEngine;

namespace StaticData.Hero.Components
{
  [CreateAssetMenu(fileName = "Hero Move Static Data", menuName = "Static Data/Hero/Create Hero Move Data", order = 55)]
  public class HeroMoveStaticData : ScriptableObject
  {
    public float MoveSpeed = 2f;
    public float RollSpeed = 2f;
  }
}