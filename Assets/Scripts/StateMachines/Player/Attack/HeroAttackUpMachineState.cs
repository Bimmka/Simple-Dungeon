using Hero;
using Services;
using StateMachines.Player.Base;

namespace StateMachines.Player.Attack
{
  public class HeroAttackUpMachineState : HeroBaseUpMachineState<HeroAttackSubState>
  {
    
    
    public HeroAttackUpMachineState(StateMachineWithSubstates stateMachine, HeroStateMachine hero, ICoroutineRunner coroutineRunner) : base(stateMachine, hero, coroutineRunner)
    {
    }
  }
}