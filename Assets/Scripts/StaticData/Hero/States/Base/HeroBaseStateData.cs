using UnityEngine;

namespace StaticData.Hero.States.Base
{
  [CreateAssetMenu(fileName = "HeroSimpleStateStaticData", menuName = "Static Data/Hero/States/Create Hero Base State Data", order = 55)]
  public class HeroBaseStateData : ScriptableObject
  {
    public HeroState State;
    public int Weight;
    public AnimationCurve EnterCurve;
    public AnimationCurve ExitCurve;
    public bool IsInteraptedBySameWeight;
  }
}