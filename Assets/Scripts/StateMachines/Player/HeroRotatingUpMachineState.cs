using Hero;
using Services;

namespace StateMachines.Player
{
  public class HeroRotatingUpMachineState : HeroBaseUpMachineState<HeroRotatingSubState>
  {
    public HeroRotatingUpMachineState(StateMachineWithSubstates stateMachine, HeroStateMachine hero, ICoroutineRunner coroutineRunner) : base(stateMachine, hero, coroutineRunner)
    {
    }
  }
}