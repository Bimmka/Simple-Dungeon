using System;
using System.Collections.Generic;

namespace StaticData.Hero.Attacks.Combo
{
  [Serializable]
  public struct Combo
  {
    public List<ComboStep> Steps;
  }
}