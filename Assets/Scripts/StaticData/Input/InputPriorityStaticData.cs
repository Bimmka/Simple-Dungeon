using System;
using System.Collections;
using System.Collections.Generic;
using InputActions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StaticData.Input
{
  [CreateAssetMenu(fileName = "InputPriorityStaticData", menuName = "Static Data/Hero/Create Input Priority Data", order = 55)]
  public class InputPriorityStaticData : SerializedScriptableObject
  {
    [ValueDropdown("Actions", IsUniqueList = true, DropdownTitle = "Select Action")]
    public List<HeroInputPriority> Priority;
    public InputActionAsset Asset;

    private IEnumerable Actions()
    {
      ValueDropdownList<HeroInputPriority> dropdownItems = new ValueDropdownList<HeroInputPriority>();

      foreach (InputActionMap actionMap in Asset.actionMaps)
      {
        foreach (InputAction inputAction in actionMap.actions)
        {
          dropdownItems.Add(new ValueDropdownItem<HeroInputPriority>(inputAction.name, new HeroInputPriority(inputAction.id)));
        }
        
      }

      return dropdownItems;
    }
  }

  [Serializable]
  public struct HeroInputPriority
  {
    public Guid Action;
    public int Priority;

    public HeroInputPriority(Guid action)
    {
      Action = action;
      Priority = 0;
    }
  }
}