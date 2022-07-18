using Animations;
using Services;
using Services.PlayerData;
using StateMachines;
using StateMachines.Player;
using StateMachines.Player.Attack;
using StateMachines.Player.Base;
using StateMachines.Player.Move;
using StateMachines.Player.Roll;
using StaticData.Hero.Attacks;
using StaticData.Hero.Components;
using StaticData.Hero.States.Base;
using UnityEngine;

namespace Hero
{
    public class HeroStateMachine : BaseEntityStateMachine, ICoroutineRunner
    {
        [SerializeField] private AnimatorStateBehaviourContainer _behaviourContainer;
        [SerializeField] private HeroAnimator _heroAnimator;
        [SerializeField] private HeroMove _move;
        [SerializeField] private HeroRotate _rotate;
        [SerializeField] private HeroAttack _attack;
        [SerializeField] private HeroStamina _stamina;
        [SerializeField] private AnimatorClipsContainer _clipsContainer;
        [SerializeField] private HeroStatesStaticData _statesData;
        
        private AttacksStaticData _attackData;
        private HeroImpactsStaticData _impactsData;
        private HeroMoveStaticData _moveData;

        private HeroMachineStatesFactory _statesFactory;
        private HeroStatesContainer _statesContainer;

        private StateMachineWithSubstates _stateMachine;

        private HeroAttacksCombo _comboObserver;
        private HeroEffectsObserver _effectsObserver;

        public bool IsBlockingPressed => false;
        public bool IsRunningPressed => false;
        public bool IsBlockingUp => false;//_stateMachine.State == State<HeroIdleShieldState>();
        public bool IsRolling => false;

        public Vector2 MoveAxis { get; private set; }


        public void Construct(AttacksStaticData attackData, HeroImpactsStaticData impactData, PlayerCharacteristics characteristics, HeroMoveStaticData moveData)
        {
            _attackData = attackData;
            _moveData = moveData;
            _impactsData = impactData;
            _attack.Construct(attackData, characteristics);
            _comboObserver = new HeroAttacksCombo(attackData.ComboStaticData, this);
            _effectsObserver = new HeroEffectsObserver(_heroAnimator);
            Initialize();
        }

        protected override void Initialize()
        {
            _clipsContainer.CollectClips();
            _behaviourContainer.Initialize();
            base.Initialize();
        }

        protected override void Subscribe()
        {
            base.Subscribe();
            _heroAnimator.Triggered += AnimationTriggered;

            HeroRollSubState rollSubState = State<HeroRollSubState>();
            rollSubState.Started += OnStartRoll;
            rollSubState.Ended += OnEndRoll;
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            _heroAnimator.Triggered -= AnimationTriggered;
            State<HeroAttackState>().Cleanup();
            
            HeroRollSubState rollSubState = State<HeroRollSubState>();
            rollSubState.Started -= OnStartRoll;
            rollSubState.Ended -= OnEndRoll;
            rollSubState.Cleanup();
        }

        protected override void Update() => 
            _stateMachine.LogicUpdate();


        protected override void CreateStates()
        {
            _statesFactory = new HeroMachineStatesFactory(_stateMachine,this, _heroAnimator, _move, _attack, _rotate, _attackData, _stamina, _impactsData, this, _moveData, _statesData, _behaviourContainer);
            _statesContainer = new HeroStatesContainer(_statesFactory);
            _statesContainer.CreateStates();
        }

        protected override void SetDefaultState() => 
            _stateMachine.Initialize(GetUpStateForSubstate(State<HeroIdleState>()), State<HeroIdleState>());

        protected override void CreateStateMachine() => 
            _stateMachine = new StateMachineWithSubstates();

        protected override void AnimationTriggered() => 
            _stateMachine.AnimationTriggered();


        public void SetAttackState(Vector3 clickPosition)
        {
            AttackType attackType = _comboObserver.NextAttack();
            if (attackType == AttackType.None)
                return;
            
            HeroAttackSubState state = AttackState(attackType);

            if (_stateMachine.State.IsCanBeInterrupted(state.Weight) && state.IsCanAttack())
            {
                Debug.Log($"Interrupted State {_stateMachine.State.GetType()}");
                Debug.Log($"Set Attack Type {attackType.ToString()}");
                state.SetClickPosition(clickPosition);
                _stateMachine.InterruptState(GetUpStateForSubstate(state), state); ;
            }
        }

        public void SetMoveAxis(Vector2 moveDirection) => 
            MoveAxis = moveDirection;

        public void SetIsBlocking(bool isBlocking)
        {
            if (IsBlockingPressed != isBlocking) 
                _effectsObserver.UpdateIsBlockingPressed(isBlocking);
        }


        public void SetRollState()
        {
            if (_stateMachine.State.IsCanBeInterrupted(State<HeroRollSubState>().Weight) && State<HeroRollSubState>().IsCanRoll())
                _stateMachine.InterruptState(GetUpStateForSubstate(State<HeroRollSubState>()), State<HeroRollSubState>());
        }

        public void SetIsRunning(bool isRunning)
        {
            if (IsRunningPressed != isRunning) 
                _effectsObserver.UpdateIsRunningPressed(isRunning);
        }

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

        public HeroAttackSubState AttackState(AttackType type) => 
            _statesContainer.GetAttackState(type);

        public IHeroBaseUpMachineState GetUpStateForSubstate(IHeroBaseSubStateMachineState state )=> 
            _statesContainer.GetUpStateForSubstate(state);

        public void FinishAttack(StateFinishType finishType) => 
            _comboObserver.AttackFinished(finishType);

        public void StartAttack() => 
            _comboObserver.StartAttack();

        private void OnStartRoll() => 
            _effectsObserver.SetIsStartRoll();

        private void OnEndRoll() => 
            _effectsObserver.SetIsEndRoll();

        private void SetAnimationCancelingState() => 
            _stateMachine.SetStateFinishType(StateFinishType.Canceling);

        private void SetAnimationInterruptingState() => 
            _stateMachine.SetStateFinishType(StateFinishType.Interrupted);

        private void SetAnimationFinishState() => 
            _stateMachine.SetStateFinishType(StateFinishType.Finish);
    }
}
