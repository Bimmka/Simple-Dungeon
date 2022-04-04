using System;
using UnityEngine;

namespace StateMachines.Player.AnimationStatesBehaviour
{
  public class RollBehaviour : BaseStateBehaviour
  {
    [SerializeField] private Vector2[] immuneRanges;
    [SerializeField] private Vector2 moveRange;
    [SerializeField] private float notInterruptedTime = 0.7f;

    public event Action<bool> Immuned;
    public event Action<bool> Moved;

    public bool IsCanBeInterrupted { get; private set; }
    

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo,
      int layerIndex)
    {
      Move(IsInMoveRange(stateInfo.normalizedTime));
      Immune(IsInImmuneRange(stateInfo.normalizedTime));
      SetInCanBeInterrupted(CheckInterrupted(stateInfo.normalizedTime));
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      base.OnStateExit(animator, stateInfo, layerIndex);
      SetInCanBeInterrupted(false);
    }

    private void Move(bool isMove) => 
      Moved?.Invoke(isMove);

    private void Immune(bool isImmune) => 
      Immuned?.Invoke(isImmune);

    private void SetInCanBeInterrupted(bool isInterrupted)
    {
      IsCanBeInterrupted = isInterrupted;
      Debug.Log($"IsInterrupted {IsCanBeInterrupted}");
    }

    private bool IsInMoveRange(float time) => 
      time >= moveRange.x && time <= moveRange.y;

    private bool CheckInterrupted(float time) => 
      time >= notInterruptedTime;

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