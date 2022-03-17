using System.Collections.Generic;
using UnityEngine;

namespace StaticData.Hero.States
{
  [CreateAssetMenu(fileName = "HeroStateStaticData", menuName = "Static Data/Hero/Create Hero State Data", order = 55)]
  public class HeroStatesStaticData : ScriptableObject
  {
    public List<HeroStateData> StateDatas;

  }
}