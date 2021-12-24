using StateMachines.Player;
using UnityEngine;

namespace Hero
{
    public class HeroStateMachine : BaseEntityStateMachine
    {
        [SerializeField] private HeroMove move;
        [SerializeField] private HeroRotate rotate;

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
        public float RotateAngle { get; private set; }

        
        protected override void CreateStates()
        {
            AttackState = new PlayerAttackState(stateMachine, "IsSimpleAttack", simpleAnimator, this);
            ImpactState = new PlayerHurtState(stateMachine, "IsImpact", simpleAnimator, this);
            IdleShieldState = new PlayerIdleShieldState(stateMachine, "IsBlocking", "MouseRotation", simpleAnimator, this, rotate);
            IdleState = new PlayerIdleState(stateMachine, "IsIdle", "MouseRotation", simpleAnimator, this, rotate);
            RollState = new PlayerRollState(stateMachine, "IsRoll", simpleAnimator, this, move);
            ShieldImpactState = new PlayerShieldImpactState(stateMachine, "IsShieldImpact", simpleAnimator, this);
            MoveState = new PlayerMoveState(stateMachine, "IsIdle", "MoveX", simpleAnimator, this, move, rotate);
            ShieldMoveState = new PlayerShieldMoveState(stateMachine, "IsBlocking", "MoveY", simpleAnimator, this, move, rotate);
            DeathState = new PlayerDeathState(stateMachine, "IsDead", simpleAnimator);
        }

        protected override void SetDefaultState() => 
            stateMachine.Initialize(IdleState);


        public void SetAttackState()
        {
            if (stateMachine.State.IsCanBeInterapted())
                stateMachine.ChangeState(AttackState);
        }

        public void SetMoveAxis(Vector2 moveDirection) => 
            MoveAxis = moveDirection;

        public void SetIsBlocking(bool isBlocking) => 
            IsBlocking = isBlocking;

        public void SetRollState()
        {
            if (stateMachine.State.IsCanBeInterapted())
                stateMachine.ChangeState(RollState);
        }

        private void AnimationTriggered() => 
            stateMachine.State.AnimationTrigger();

        public void SetRotate(float rotateAngle) => 
            RotateAngle = rotateAngle;
    }
}
