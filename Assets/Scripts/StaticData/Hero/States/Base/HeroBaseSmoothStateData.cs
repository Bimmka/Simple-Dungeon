using UnityEngine;

namespace StaticData.Hero.States.Base
{
  [CreateAssetMenu(fileName = "HeroBaseSmoothStateStaticData", menuName = "Static Data/Hero/States/Create Hero Base Smooth State Data", order = 55)]
  public class HeroBaseSmoothStateData : HeroBaseStateData
  {
    public AnimationCurve EnterCurve;
    public AnimationCurve ExitCurve;
  }
}