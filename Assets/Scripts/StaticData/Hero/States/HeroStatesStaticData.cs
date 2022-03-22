using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace StaticData.Hero.States
{
  [CreateAssetMenu(fileName = "HeroStateStaticData", menuName = "Static Data/Hero/Create Hero State Data", order = 55)]
  public class HeroStatesStaticData : ScriptableObject
  {
    [ValidateInput("IsNotHaveDuplicate", "Cannot set same weight", InfoMessageType.Error)]
    public List<HeroStateWithSubstates> StateDatas;
    
    #if UNITY_EDITOR
    private bool IsNotHaveDuplicate()
    {
      for (int i = 0; i < StateDatas.Count-1; i++)
      {
        for (int k = 0; k < StateDatas[i].SubstatesData.Length; k++)
        {
          if (IsNotHaveDuplicateInSameUpState(i, k) == false || IsNotHaveDuplicatedInOtherUpState(i, k) == false)
            return false;
        }
      }

      return true;
    }

    private bool IsNotHaveDuplicatedInOtherUpState(int baseUpStateIndex, int subStateIndex)
    {
      for (int i = baseUpStateIndex+1; i < StateDatas.Count; i++)
      {
        for (int j = 0; j < StateDatas[i].SubstatesData.Length; j++)
        {
          if (StateDatas[i].SubstatesData[j].Weight == StateDatas[baseUpStateIndex].SubstatesData[subStateIndex].Weight)
            return false;
        }
      }

      return true;
    }

    private bool IsNotHaveDuplicateInSameUpState(int upStateIndex, int subStateIndex)
    {
      for (int i = subStateIndex + 1; i < StateDatas[upStateIndex].SubstatesData.Length - 1; i++)
      {
        if (StateDatas[upStateIndex].SubstatesData[i].Weight == StateDatas[upStateIndex].SubstatesData[subStateIndex].Weight)
          return false;
      }

      return true;
    }
#endif
  }
}