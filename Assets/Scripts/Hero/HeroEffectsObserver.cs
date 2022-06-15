using Animations;
using UnityEngine;

namespace Hero
{
  public class HeroEffectsObserver
  {
    private static readonly int BlockAnimationID =  Animator.StringToHash("IsBlocking");
    private readonly HeroAnimator _animator;
        
    public bool IsBlocking { get; private set; }
    public bool IsRunning { get; private set; }
    public bool IsRolling { get; private set; }

    public HeroEffectsObserver(HeroAnimator animator)
    {
      _animator = animator;
    }
        
    public void UpdateIsBlockingPressed(bool isBlocking)
    {
      IsBlocking = isBlocking;
      _animator.SetBool(BlockAnimationID, isBlocking);
    }

    public void UpdateIsRunningPressed(bool isRunning)
    {
      IsRunning = isRunning;
    }

    public void SetIsStartRoll() => 
      IsRolling = true;

    public void SetIsEndRoll() => 
      IsRolling = false;
  }
}