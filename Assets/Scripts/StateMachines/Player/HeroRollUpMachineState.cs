using Hero;
using Services;

namespace StateMachines.Player
{
  public abstract class HeroRollUpMachineState : HeroBaseUpMachineState<HeroRollState>
  {
    public HeroRollUpMachineState(StateMachineWithSubstates stateMachine, HeroStateMachine hero, ICoroutineRunner coroutineRunner) : base(stateMachine, hero, coroutineRunner)
    {
    }
  }
}