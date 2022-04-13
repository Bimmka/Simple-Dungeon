using System.Collections.Generic;
using UnityEngine;

namespace StaticData.Hero.Attacks.Combo
{
  [CreateAssetMenu(fileName = "HeroCombosStaticData", menuName = "Static Data/Attacks/Combos/Create Combos Data", order = 55)]
  public class HeroCombosStaticData : ScriptableObject
  {
    public List<Combo> Combos;

    public ComboStep EmptyStep;
  }
}