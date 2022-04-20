using Animations;
using Hero;
using StateMachines.Player.AnimationStatesBehaviour;
using StateMachines.Player.Base;
using StateMachines.Player.Move;
using StaticData.Hero.States.Base;
using UnityEngine;

namespace StateMachines.Player.Roll
{
  public class HeroRollSubState : HeroBaseSubStateMachineState<HeroRollUpMachineState, HeroRollSubState, HeroBaseStateData, RollBehaviour>
  {
    private readonly HeroMove _heroMove;
    private readonly HeroStamina _heroStamina;

    public bool IsImmune { get; private set; }
    public bool IsMove { get; private set; }
    
    public HeroRollSubState(HeroRollUpMachineState upState, HeroStateMachine hero, BattleAnimator animator,
      string triggerName, HeroBaseStateData stateData,RollBehaviour rollBehaviour, HeroMove heroMove, HeroStamina heroStamina) : base(upState, hero, animator, triggerName, stateData,rollBehaviour)
    {
      _heroMove = heroMove;
      _heroStamina = heroStamina;
      behaviour.Immuned += SetImmune;
      behaviour.Moved += SetIsMove;
    }

    public override void Enter()
    {
      base.Enter();
      _heroStamina.WasteToRoll();
    }

    public override void LogicUpdate()
    {
      base.LogicUpdate();
      if (IsMove)
        _heroMove.Roll();
    }

    public override void AnimationTriggered()
    {
      base.AnimationTriggered();
      if (hero.IsBlockingPressed)
      {
        if (hero.IsNotMove())
          ChangeState(hero.State<HeroIdleShieldState>());
        else
          ChangeState(hero.State<HeroShieldMoveState>());
      }
      else
      {
        if (hero.IsNotMove())
          ChangeState(hero.State<HeroIdleState>());
        else if (hero.IsRunningPressed == false)
          ChangeState(hero.State<HeroWalkState>());
        else 
          ChangeState(hero.State<HeroRunState>());
      }
    }

    public override bool IsCanBeInterrupted(int weight) => 
      base.IsCanBeInterrupted(weight) && behaviour.IsCanBeInterrupted;

    public void Cleanup()
    {
      behaviour.Immuned -= SetImmune;
      behaviour.Moved -= SetIsMove;
    }

    public bool IsCanRoll() => 
      _heroStamina.IsCanRoll();

    private void SetImmune(bool isImmune) => 
      IsImmune = isImmune;

    private void SetIsMove(bool isMove) => 
      IsMove = isMove;
  }
}