using Animations;
using Services;
using Services.PlayerData;
using StateMachines;
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

        private StateMachineWithSubstates _stateMachine;

        public bool IsBlockingPressed { get; private set; }
        public bool IsRunningPressed { get; private set; }
        public bool IsBlockingUp => false;//_stateMachine.State == State<HeroIdleShieldState>();
        public bool IsRolling => _stateMachine.State.IsSameState(State<HeroRollState>());

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
//            State<PlayerAttackState>().Cleanup();
        }

        protected override void Update() => 
            _stateMachine.LogicUpdate();


        protected override void CreateStates()
        {
            _statesFactory = new HeroMachineStatesFactory(_stateMachine,this, _battleAnimator, _move, _attack, _rotate, _attackData, _stamina, _impactsData, this, _moveData, _statesData);
            _statesContainer = new HeroStatesContainer(_statesFactory);
            _statesContainer.CreateState();
        }

        protected override void SetDefaultState() => 
            _stateMachine.Initialize(GetUpStateForSubstate(State<HeroIdleState>()), State<HeroIdleState>());

        protected override void CreateStateMachine() => 
            _stateMachine = new StateMachineWithSubstates();

        protected override void AnimationTriggered() => 
            _stateMachine.AnimationTriggered();


        public void SetAttackState()
        {
        }

        public void SetMoveAxis(Vector2 moveDirection) => 
            MoveAxis = moveDirection;

        public void SetIsBlocking(bool isBlocking) => 
            IsBlockingPressed = isBlocking;

        public void SetRollState()
        {
            if (_stateMachine.State.IsCanBeInterrupted(State<HeroRollState>().Weight) && State<HeroRollState>().IsCanRoll())
                _stateMachine.InterruptState(GetUpStateForSubstate(State<HeroRollState>()), State<HeroRollState>());
        }

        public void SetIsRunning(bool isRunning) => 
            IsRunningPressed = isRunning;

        public void ImpactInShield()
        {
            
        }

        public void Impact()
        {
            
        }

        public void Dead()
        {
            
        }

        public bool IsStayHorizontal() => 
            Mathf.Approximately(MoveAxis.x, 0);

        public bool IsStayVertical() => 
            Mathf.Approximately(MoveAxis.y, 0);

        public bool IsNotMove() => 
            MoveAxis == Vector2.zero;

        public float ClipLength(PlayerActionsType actionsType) => 
            _clipsContainer.ClipLength(actionsType);

        public TState State<TState>() where TState : IHeroBaseSubStateMachineState => 
            (TState) _statesContainer.GetState<TState>();

        public IHeroBaseUpMachineState GetUpStateForSubstate(IHeroBaseSubStateMachineState state )=> 
            _statesContainer.GetUpStateForSubstate(state);
    }
}
