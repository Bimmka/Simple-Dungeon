using Animations;
using Services;
using Services.PlayerData;
using StateMachines.Player;
using StaticData.Hero.Components;
using UnityEngine;

namespace Hero
{
    public class HeroStateMachine : BaseEntityStateMachine, ICoroutineRunner
    {
        [SerializeField] private BattleAnimator battleAnimator;
        [SerializeField] private HeroMove move;
        [SerializeField] private HeroRotate rotate;
        [SerializeField] private HeroAttack attack;
        [SerializeField] private HeroStamina stamina;
        
        private HeroAttackStaticData attackData;
        private HeroImpactsStaticData impactsData;

        private HeroMachineStatesFactory statesFactory;
        private HeroStatesContainer statesContainer;
        private HeroMoveStaticData moveData;

        public bool IsBlockingPressed { get; private set; }
        public bool IsBlockingUp => stateMachine.State == State<PlayerIdleShieldState>();
        public bool IsRolling => stateMachine.State == State<PlayerRollState>();

        public Vector2 MoveAxis { get; private set; }
        public float RotateAngle { get; private set; }

        public void Construct(HeroAttackStaticData attackData, HeroImpactsStaticData impactData, PlayerCharacteristics characteristics, HeroMoveStaticData moveData)
        {
            this.attackData = attackData;
            this.moveData = moveData;
            impactsData = impactData;
            attack.Construct(attackData, characteristics);
            Initialize();
        }

        protected override void Subscribe()
        {
            base.Subscribe();
            battleAnimator.Triggered += AnimationTriggered;
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            battleAnimator.Triggered -= AnimationTriggered;
            State<PlayerAttackState>().Cleanup();
        }


        protected override void CreateStates()
        {
            statesFactory = new HeroMachineStatesFactory(stateMachine,this, battleAnimator, move, attack, rotate, attackData, stamina, impactsData, this, moveData);
            statesContainer = new HeroStatesContainer(statesFactory);
            statesContainer.CreateState();
        }

        protected override void SetDefaultState() => 
            stateMachine.Initialize(State<PlayerIdleState>());


        public void SetAttackState()
        {
            if (stateMachine.State.IsCanBeInterapted() && State<PlayerAttackState>().IsCanAttack())
                stateMachine.ChangeState(State<PlayerAttackState>());
        }

        public void SetMoveAxis(Vector2 moveDirection) => 
            MoveAxis = moveDirection;

        public void SetIsBlocking(bool isBlocking) => 
            IsBlockingPressed = isBlocking;

        public void SetRollState()
        {
            if (stateMachine.State.IsCanBeInterapted() && State<PlayerRollState>().IsCanRoll())
                stateMachine.ChangeState(State<PlayerRollState>());
        }

        public void ImpactInShield() => 
            stateMachine.ChangeState(State<PlayerShieldImpactState>());

        public void Impact()
        {
            if (State<PlayerBaseImpactState>().IsKnockbackCooldown() && stateMachine.State.IsCanBeInterapted())
                stateMachine.ChangeState(State<PlayerBaseImpactState>());
        }

        public void Dead() => 
            stateMachine.ChangeState(State<PlayerDeathState>());

        public TState State<TState>() where TState : PlayerBaseMachineState => 
            statesContainer.GetState<TState>();
    }
}
