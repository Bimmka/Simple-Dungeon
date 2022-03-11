using System;
using Hero;
using StaticData.Hero.Components;
using TMPro;
using UnityEngine;

namespace Debugging
{
  public class DebugHeroComponentsValue : MonoBehaviour
  {
    [SerializeField] private TMP_InputField maxAngleField;
    [SerializeField] private TMP_InputField interpolateValueField;
    [SerializeField] private HeroMoveStaticData moveData;
    [SerializeField] private HeroRotate heroRotate;

    private void Awake()
    {
      maxAngleField.onEndEdit.AddListener(OnMaxAngleChange);
      interpolateValueField.onEndEdit.AddListener(OnInterpolateChange);
    }

    private void OnDestroy()
    {
      maxAngleField.onEndEdit.RemoveListener(OnMaxAngleChange);
      interpolateValueField.onEndEdit.RemoveListener(OnInterpolateChange);
    }

    private void OnMaxAngleChange(string text)
    {
      if (String.IsNullOrEmpty(text) || float.TryParse(text, out float value) == false)
        return;

      moveData.BigAngleValue = value;
    }

    private void OnInterpolateChange(string text)
    {
      if (String.IsNullOrEmpty(text) || float.TryParse(text, out float value) == false)
        return;

#if DEBUG_MOVE
      heroRotate.SetInterpolateValue(Mathf.Clamp01(value));
#endif
      
    }

    public void Construct(HeroRotate component)
    {
      heroRotate = component;
    }
  }
}