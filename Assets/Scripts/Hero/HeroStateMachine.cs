using System;
using Animations;
using StateMachines;
using StateMachines.Player;
using UnityEngine;

namespace Hero
{
    public class HeroStateMachine : MonoBehaviour
    {
        [SerializeField] private SimpleAnimator animator;
        
        private StateMachine stateMachine;
        
        public PlayerAttackState AttackState { get; private set; }
        public PlayerHurtState ImpactState { get; private set; }
        public PlayerIdleShieldState IdleShieldState { get; private set; }
        public PlayerIdleState IdleState { get; private set; }
        public PlayerRollState RollState { get; private set; }
        public PlayerShieldImpactState ShieldImpactState { get; private set; }
        public PlayerMoveState MoveState { get; private set; }
        public PlayerShieldMoveState ShieldMoveState { get; private set; }
        public PlayerDeathState DeathState { get; private set; }
        
        public bool IsBlocking { get; private set; }
        
        public Vector2 MoveAxis { get; private set; }

        private void Awake()
        {
            CreateStateMachine();
            CreateStates();
            SetDefaultState();
            Subscribe();
        }

        private void OnDestroy() => 
            Cleanup();

        private void Update() => 
            stateMachine.State.LogicUpdate();

        private void Subscribe() => 
            animator.Triggered += AnimationTriggered;

        private void Cleanup() => 
            animator.Triggered -= AnimationTriggered;

        private void CreateStateMachine() => 
            stateMachine = new StateMachine();

        private void CreateStates()
        {
            AttackState = new PlayerAttackState(stateMachine, "IsSimpleAttack", animator, this);
            ImpactState = new PlayerHurtState(stateMachine, "IsImpact", animator);
            IdleShieldState = new PlayerIdleShieldState(stateMachine, "IsShieldIdle", animator);
            IdleState = new PlayerIdleState(stateMachine, "IsIdle", animator, this);
            RollState = new PlayerRollState(stateMachine, "IsRoll", animator);
            ShieldImpactState = new PlayerShieldImpactState(stateMachine, "IsShieldImpact", animator);
            MoveState = new PlayerMoveState(stateMachine, "IsWalk", animator, this);
            ShieldMoveState = new PlayerShieldMoveState(stateMachine, "IsShieldWalk", animator);
            DeathState = new PlayerDeathState(stateMachine, "IsDead", animator);
        }
        private void SetDefaultState() => 
            stateMachine.Initialize(IdleState);

        public void SetAttackState()
        {
            if (stateMachine.State.IsCanBeInterapted())
                stateMachine.ChangeState(AttackState);
        }

        public void SetWalkState(Vector2 moveDirection)
        {
            MoveAxis = moveDirection;
            Debug.Log(MoveAxis);
        }

        public void SetIsBlocking(bool isBlocking) => 
            IsBlocking = isBlocking;

        public void SetRollState()
        {
            if (stateMachine.State.IsCanBeInterapted())
                stateMachine.ChangeState(RollState);
        }

        private void AnimationTriggered() => 
            stateMachine.State.AnimationTrigger();
    }
}
