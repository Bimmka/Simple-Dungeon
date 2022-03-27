using System;

namespace StaticData.Hero.States.Base
{
  [Serializable]
  public struct HeroStateWithSubstates
  {
    public HeroParentStateType UpState;
    public HeroBaseStateData[] SubstatesData;
  }
}