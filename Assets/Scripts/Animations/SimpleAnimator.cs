using System;
using UnityEngine;

namespace Animations
{
  public class SimpleAnimator : MonoBehaviour
  {
    [SerializeField] private Animator animator;

    public event Action Triggered; 

    public void SetBool(int animationHash, bool isSet) => 
      animator.SetBool(animationHash, isSet);

    public void AnimationTriggered() => 
      Triggered?.Invoke();
  }
}