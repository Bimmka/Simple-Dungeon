using System;
using System.Collections.Generic;
using Animations;
using Services;
using StateMachines;
using StateMachines.Player;
using StaticData.Hero.Components;

namespace Hero
{
  public class HeroMachineStatesFactory
  {
    private readonly StateMachine _stateMachine;
    private readonly HeroStateMachine _hero;
    private readonly BattleAnimator _animator;
    private readonly HeroMove _move;
    private readonly HeroAttack _attack;
    private readonly HeroRotate _rotate;
    private readonly HeroAttackStaticData _attackData;
    private readonly HeroStamina _stamina;
    private readonly HeroImpactsStaticData _impactData;
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly HeroMoveStaticData _moveStaticData;
    private readonly AnimatorClipsContainer _clipsContainer;


    public HeroMachineStatesFactory(StateMachine stateMachine, HeroStateMachine hero, BattleAnimator animator,
      HeroMove move,
      HeroAttack attack, HeroRotate rotate, HeroAttackStaticData attackData, HeroStamina stamina,
      HeroImpactsStaticData impactData, ICoroutineRunner coroutineRunner, HeroMoveStaticData moveStaticData, AnimatorClipsContainer clipsContainer)
    {
      _stateMachine = stateMachine;
      _hero = hero;
      _animator = animator;
      _move = move;
      _attack = attack;
      _rotate = rotate;
      _attackData = attackData;
      _stamina = stamina;
      _impactData = impactData;
      _coroutineRunner = coroutineRunner;
      _moveStaticData = moveStaticData;
      _clipsContainer = clipsContainer;
    }

    public void CreateStates(ref Dictionary<Type, PlayerBaseMachineState> states)
    {
      states.Add(typeof(PlayerAttackState),new PlayerAttackState(_stateMachine, "IsSimpleAttack", _animator, _hero, _attack, _attackData,
        _stamina));
      states.Add(typeof(PlayerHurtState),new PlayerHurtState(_stateMachine, "IsImpact", _animator, _hero, _impactData.ImpactCooldown));
      states.Add(typeof(PlayerIdleShieldState),new PlayerIdleShieldState(_stateMachine, "IsBlocking", _animator, _hero));
      states.Add(typeof(PlayerIdleState), new PlayerIdleState(_stateMachine, "IsIdle", _animator, _hero));
      states.Add(typeof(PlayerRollState), new PlayerRollState(_stateMachine, "IsRoll", _animator, _hero, _move, _stamina));
      states.Add(typeof(PlayerShieldImpactState), new PlayerShieldImpactState(_stateMachine, "IsShieldImpact", _animator, _hero, _impactData.ShieldImpactCooldown));
      states.Add(typeof(PlayerMoveState), new PlayerMoveState(_stateMachine, "IsIdle", "MoveX", _animator, _hero, _move, _rotate, _coroutineRunner, _moveStaticData));
      states.Add(typeof(PlayerShieldMoveState), new PlayerShieldMoveState(_stateMachine, "IsBlocking", "MoveY", _animator, _hero, _move, _rotate));
      states.Add(typeof(PlayerDeathState), new PlayerDeathState(_stateMachine, "IsDead", _animator, _hero));
      states.Add(typeof(PlayerRotatingState), new PlayerRotatingState(_stateMachine, "IsRotating", _animator, _hero, "MoveX", "MoveY", _rotate, _clipsContainer));
    }
  }
}