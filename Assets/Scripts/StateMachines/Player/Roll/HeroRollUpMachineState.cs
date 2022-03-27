using Hero;
using Services;
using StateMachines.Player.Base;

namespace StateMachines.Player.Roll
{
  public class HeroRollUpMachineState : HeroBaseUpMachineState<HeroRollSubState>
  {
    public HeroRollUpMachineState(StateMachineWithSubstates stateMachine, HeroStateMachine hero, ICoroutineRunner coroutineRunner) : base(stateMachine, hero, coroutineRunner)
    {
    }
  }
}