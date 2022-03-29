using System;
using UnityEngine;

namespace StateMachines.Player.AnimationStatesBehaviour
{
  public class RollBehaviour : BaseStateBehaviour
  {
    [SerializeField] private Vector2[] immuneRanges;
    [SerializeField] private Vector2 moveRange;
    
    public event Action<bool> Immuned;
    public event Action<bool> Moved;

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo,
      int layerIndex)
    {
      Move(IsInMoveRange(stateInfo.normalizedTime));
      Immune(IsInImmuneRange(stateInfo.normalizedTime));
    }

    public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo,
      int layerIndex)
    {
    }

    public override void OnStateIK(Animator animator, AnimatorStateInfo stateInfo,
      int layerIndex)
    {
    }

    private void Move(bool isMove) => 
      Moved?.Invoke(isMove);

    private void Immune(bool isImmune) => 
      Immuned?.Invoke(isImmune);

    private bool IsInMoveRange(float time) => 
      time >= moveRange.x && time <= moveRange.y;

    private bool IsInImmuneRange(float time)
    {
      for (int i = 0; i < immuneRanges.Length; i++)
      {
        if (time >= immuneRanges[i].x && time <= immuneRanges[i].y)
          return true;
      }

      return false;
    }
  }
}