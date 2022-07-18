using System.Collections.Generic;
using Animations;
using UnityEngine;

namespace Hero
{
  public class HeroEffectsObserver
  {
    private static readonly int BlockAnimationID =  Animator.StringToHash("IsBlocking");
    private readonly HeroAnimator _animator;

    private readonly List<InfluencingEffectType> influencingEffects;

    public HeroEffectsObserver(HeroAnimator animator)
    {
      _animator = animator;
      influencingEffects = new List<InfluencingEffectType>(10);
    }

    public bool IsContainEffect(InfluencingEffectType type)
    {
      return influencingEffects.Contains(type);  
    }
        
    public void UpdateIsBlockingPressed(bool isBlocking)
    {
      //IsBlocking = isBlocking;
      _animator.SetBool(BlockAnimationID, isBlocking);
    }

    public void UpdateIsRunningPressed(bool isRunning)
    {
      //IsRunning = isRunning;
    }

    public void SetIsStartRoll()
    {
      
    }
    //IsRolling = true;

    public void SetIsEndRoll()
    {
      
    }
      //IsRolling = false;
  }

  public enum InfluencingEffectType
  {
    None = 0,
    Blocking,
    Running,
    Rolling,
    
  }
}