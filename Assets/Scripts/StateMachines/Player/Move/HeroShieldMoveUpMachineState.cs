using Animations;
using Hero;
using Services;
using StateMachines.Player.Base;

namespace StateMachines.Player.Move
{
  public class HeroShieldMoveUpMachineState :  HeroBaseMoveUpMachineState
  {
    public HeroShieldMoveUpMachineState(StateMachineWithSubstates stateMachine, HeroStateMachine hero, ICoroutineRunner coroutineRunner, BattleAnimator animator, string moveHashName) : base(stateMachine, hero, coroutineRunner, animator, moveHashName)
    {
    }

    protected override bool IsTransferToIdleState(IHeroBaseSubStateMachineState to) => 
      to is HeroIdleShieldState;

    protected override bool IsTransferToWalkState(IHeroBaseSubStateMachineState to) => 
      to is HeroShieldMoveState;

    protected override void TransferToIdleState()
    {
      if (currentState is HeroShieldMoveState)
        SmoothChange(SubState<HeroShieldMoveState>().ExitCurve);
    }

    protected override void TransferToWalkState()
    {
      if (currentState is HeroIdleShieldState)
        SmoothChange(SubState<HeroIdleShieldState>().EnterCurve);
    }
  }
}