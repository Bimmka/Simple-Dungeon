using System;
using UnityEngine;

namespace StaticData.Hero.States
{
  [Serializable]
  public struct HeroStateData
  {
    public HeroState State;
    public int Weight;
    public AnimationCurve EnterCurve;
    public AnimationCurve ExitCurve;
    public bool IsInteraptedBySameWeight;
  }
}