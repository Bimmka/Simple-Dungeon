using System.Collections.Generic;
using UnityEngine;

namespace StaticData.Hero.States.Base
{
  [CreateAssetMenu(fileName = "HeroStatesStaticData", menuName = "Static Data/Hero/Create Hero States Data", order = 55)]
  public class HeroStatesStaticData : ScriptableObject
  {
    public List<HeroStateWithSubstates> StateDatas;
  }
}