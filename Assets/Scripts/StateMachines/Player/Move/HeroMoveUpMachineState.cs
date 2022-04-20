using Animations;
using Hero;
using Services;
using StateMachines.Player.Base;

namespace StateMachines.Player.Move
{
  public class HeroMoveUpMachineState : HeroBaseMoveUpMachineState
  {

    public HeroMoveUpMachineState(StateMachineWithSubstates stateMachine, HeroStateMachine hero,
      ICoroutineRunner coroutineRunner, BattleAnimator animator, string moveHashName) : base(stateMachine, hero, coroutineRunner, animator, moveHashName)
    {
    }


    protected override void SetNewSubstate(IHeroBaseSubStateMachineState to)
    {
      if (IsTransferToRunState(to))
        TransferToRunState();
      
      base.SetNewSubstate(to);
    }

    protected override bool IsTransferToIdleState(IHeroBaseSubStateMachineState to) => 
      to is HeroIdleState;

    protected override bool IsTransferToWalkState(IHeroBaseSubStateMachineState to) => 
      to is HeroWalkState;

    protected override void TransferToIdleState()
    {
      if (currentState is HeroWalkState)
        SmoothChange(SubState<HeroWalkState>().ExitCurve);
      else if (currentState is HeroRunState) 
        SmoothChange(SubState<HeroRunState>().DownStateEnterCurve);
    }

    protected override void TransferToWalkState()
    {
      if (currentState is HeroIdleState)
        SmoothChange(SubState<HeroWalkState>().EnterCurve);
      else if (currentState is HeroRunState) 
        SmoothChange(SubState<HeroRunState>().UpStateEnterCurve);
    }

    private bool IsTransferToRunState(IHeroBaseSubStateMachineState to) => 
      to is HeroRunState;

    private void TransferToRunState()
    {
      if (currentState is HeroWalkState)
        SmoothChange(SubState<HeroWalkState>().UpStateEnterCurve);
      else if (currentState is HeroIdleState) 
        SmoothChange(SubState<HeroIdleState>().UpStateEnterCurve);
    }
  }
}