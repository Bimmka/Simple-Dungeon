using UnityEngine;

namespace StaticData.Hero.States
{
  [CreateAssetMenu(fileName = "HeroStateStaticData", menuName = "Static Data/Hero/Create Hero State Data", order = 55)]
  public class HeroStateStaticData : ScriptableObject
  {
    public HeroState State;
    public int Weight;
    public AnimationCurve EnterCurve;
    public AnimationCurve ExitCurve;
    public bool IsInteraptedBySameWeight = true;
  }
}