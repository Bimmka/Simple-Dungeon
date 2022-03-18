using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace StaticData.Hero.States
{
  [CreateAssetMenu(fileName = "HeroStateStaticData", menuName = "Static Data/Hero/Create Hero State Data", order = 55)]
  public class HeroStatesStaticData : ScriptableObject
  {
    [ValidateInput("IsNotHaveDuplicate", "Cannot set same weight", InfoMessageType.Error)]
    public List<HeroStateData> StateDatas;
    
    #if UNITY_EDITOR
    private bool IsNotHaveDuplicate()
    {
      for (int i = 0; i < StateDatas.Count-1; i++)
      {
        for (int j = i+1; j < StateDatas.Count; j++)
        {
          if (StateDatas[i].Weight == StateDatas[j].Weight)
            return false;
        }
      }

      return true;
    }
#endif
  }
}