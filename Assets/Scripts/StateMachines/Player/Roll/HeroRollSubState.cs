using Animations;
using Hero;
using StateMachines.Player.AnimationStatesBehaviour;
using StateMachines.Player.Base;
using StateMachines.Player.Move;
using StaticData.Hero.States.Base;

namespace StateMachines.Player.Roll
{
  public class HeroRollSubState : HeroBaseSubStateMachineState<HeroRollUpMachineState, HeroRollSubState, HeroBaseStateData>
  {
    private readonly HeroMove heroMove;
    private readonly HeroStamina heroStamina;
    private readonly RollBehaviour _rollBehaviour;

    public bool IsImmune { get; private set; }
    public bool IsMove { get; private set; }
    
    public HeroRollSubState(HeroRollUpMachineState upState, HeroStateMachine hero, BattleAnimator animator,
      string triggerName, HeroBaseStateData stateData, HeroMove heroMove, HeroStamina heroStamina, RollBehaviour rollBehaviour) : base(upState, hero, animator, triggerName, stateData)
    {
      this.heroMove = heroMove;
      this.heroStamina = heroStamina;
      _rollBehaviour = rollBehaviour;
      _rollBehaviour.Immuned += SetImmune;
      _rollBehaviour.Moved += SetIsMove;
    }

    public override void Enter()
    {
      base.Enter();
      heroStamina.WasteToRoll();
    }

    public override void LogicUpdate()
    {
      base.LogicUpdate();
      if (IsMove)
        heroMove.Roll();
    }

    public override void AnimationTriggered()
    {
      base.AnimationTriggered();
      if (hero.IsBlockingPressed)
      {
       /* if (hero.IsStayHorizontal() == false)
          ChangeState(hero.State<HeroShieldMoveState>());
        else
          ChangeState(hero.State<HeroIdleShieldState>());*/
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

    public void Cleanup()
    {
      _rollBehaviour.Immuned -= SetImmune;
      _rollBehaviour.Moved -= SetIsMove;
    }

    public bool IsCanRoll() => 
      heroStamina.IsCanRoll();

    private void SetImmune(bool isImmune) => 
      IsImmune = isImmune;

    private void SetIsMove(bool isMove) => 
      IsMove = isMove;
  }
}