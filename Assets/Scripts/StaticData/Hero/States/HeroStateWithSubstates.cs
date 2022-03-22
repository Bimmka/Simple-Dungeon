using System;

namespace StaticData.Hero.States
{
  [Serializable]
  public struct HeroStateWithSubstates
  {
    public HeroUpState UpState;
    public HeroStateData[] SubstatesData;
  }
}