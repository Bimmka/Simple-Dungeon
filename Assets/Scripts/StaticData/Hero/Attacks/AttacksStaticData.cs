using System.Collections.Generic;
using Sirenix.OdinInspector;
using StateMachines.Player.Attack;
using UnityEngine;

namespace StaticData.Hero.Attacks
{
  [CreateAssetMenu(fileName = "AttacksStaticData", menuName = "Static Data/Attacks/Create Attacks Data", order = 55)]
  public class AttacksStaticData : SerializedScriptableObject
  {
    [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.Foldout, KeyLabel = "Attack Type", ValueLabel = "Attack Data")]
    public Dictionary<AttackType, AttackStaticData> AttacksData;
    
    public int MaxAttackedEntitiesCount;
    public LayerMask Mask;
  }
}