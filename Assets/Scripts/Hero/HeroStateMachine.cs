using Animations;
using Services;
using Services.PlayerData;
using StateMachines.Player;
using StaticData.Hero.Components;
using StaticData.Hero.States;
using UnityEngine;

namespace Hero
{
    public class HeroStateMachine : BaseEntityStateMachine, ICoroutineRunner
    {
        [SerializeField] private BattleAnimator _battleAnimator;
        [SerializeField] private HeroMove _move;
        [SerializeField] private HeroRotate _rotate;
        [SerializeField] private HeroAttack _attack;
        [SerializeField] private HeroStamina _stamina;
        [SerializeField] private AnimatorClipsContainer _clipsContainer;
        [SerializeField] private HeroStatesStaticData _statesData;
        
        private HeroAttackStaticData _attackData;
        private HeroImpactsStaticData _impactsData;

        private HeroMachineStatesFactory _statesFactory;
        private HeroStatesContainer _statesContainer;
        private HeroMoveStaticData _moveData;

        public bool IsBlockingPressed { get; private set; }
        public bool IsBlockingUp => stateMachine.State == State<PlayerIdleShieldState>();
        public bool IsRolling => stateMachine.State == State<PlayerRollState>();

        public Vector2 MoveAxis { get; private set; }
        public float RotateAngle { get; private set; }

        public void Construct(HeroAttackStaticData attackData, HeroImpactsStaticData impactData, PlayerCharacteristics characteristics, HeroMoveStaticData moveData)
        {
            _attackData = attackData;
            _moveData = moveData;
            _impactsData = impactData;
            _attack.Construct(attackData, characteristics);
            Initialize();
        }

        protected override void Initialize()
        {
            _clipsContainer.CollectClips();
            base.Initialize();
        }

        protected override void Subscribe()
        {
            base.Subscribe();
            _battleAnimator.Triggered += AnimationTriggered;
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            _battleAnimator.Triggered -= AnimationTriggered;
            State<PlayerAttackState>().Cleanup();
        }


        protected override void CreateStates()
        {
            _statesFactory = new HeroMachineStatesFactory(stateMachine,this, _battleAnimator, _move, _attack, _rotate, _attackData, _stamina, _impactsData, this, _moveData, _statesData);
            _statesContainer = new HeroStatesContainer(_statesFactory);
            _statesContainer.CreateState();
        }

        protected override void SetDefaultState() => 
            stateMachine.Initialize(State<PlayerIdleState>());


        public void SetAttackState()
        {
            if (stateMachine.State.IsCanBeInterapted(State<PlayerAttackState>().Weight) && State<PlayerAttackState>().IsCanAttack())
                stateMachine.ChangeState(State<PlayerAttackState>());
        }

        public void SetMoveAxis(Vector2 moveDirection) => 
            MoveAxis = moveDirection;

        public void SetIsBlocking(bool isBlocking) => 
            IsBlockingPressed = isBlocking;

        public void SetRollState()
        {
            if (stateMachine.State.IsCanBeInterapted(State<PlayerRollState>().Weight) && State<PlayerRollState>().IsCanRoll())
                stateMachine.ChangeState(State<PlayerRollState>());
        }

        public void ImpactInShield() => 
            stateMachine.ChangeState(State<PlayerShieldImpactState>());

        public void Impact()
        {
            if (State<PlayerBaseImpactState>().IsKnockbackCooldown() && stateMachine.State.IsCanBeInterapted(State<PlayerBaseImpactState>().Weight))
                stateMachine.ChangeState(State<PlayerBaseImpactState>());
        }

        public void Dead() => 
            stateMachine.ChangeState(State<PlayerDeathState>());

        public float ClipLength(PlayerActionsType actionsType) => 
            _clipsContainer.ClipLength(actionsType);

        public TState State<TState>() where TState : PlayerBaseMachineState => 
            _statesContainer.GetState<TState>();
    }
}
