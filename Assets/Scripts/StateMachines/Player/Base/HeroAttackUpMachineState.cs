using Hero;
using Services;

namespace StateMachines.Player.Base
{
  public class HeroAttackUpMachineState : HeroBaseUpMachineState<HeroAttackSubState>
  {
    public HeroAttackUpMachineState(StateMachineWithSubstates stateMachine, HeroStateMachine hero, ICoroutineRunner coroutineRunner) : base(stateMachine, hero, coroutineRunner)
    {
    }
  }
}