using Animations;
using Hero;
using StaticData.Hero.States.Base;
using UnityEngine;

namespace StateMachines.Player
{
  public abstract class HeroRotatingSubState : HeroBaseSubStateMachineState<HeroRotatingUpMachineState, HeroRotatingSubState, HeroRotateStateData>
  {
    public AnimationCurve EnterCurve => stateData.EnterCurve;
    public AnimationCurve ExitCurve => stateData.ExitCurve;

    public AnimationCurve LeftTurnEnterCurve => stateData.LeftTurnEnterCurve;
    public AnimationCurve LeftTurnExitCurve => stateData.LeftTurnEnterCurve;
    public AnimationCurve RightTurnEnterCurve => stateData.LeftTurnEnterCurve;
    public AnimationCurve RightTurnExitCurve => stateData.LeftTurnEnterCurve;
    public AnimationCurve TurnAroundEnterCurve => stateData.LeftTurnEnterCurve;
    public AnimationCurve TurnAroundExitCurve => stateData.LeftTurnEnterCurve;

    public PlayerActionsType LastActionType { get; protected set; }

    public HeroRotatingSubState(HeroRotatingUpMachineState _upState, HeroStateMachine hero, BattleAnimator animator, string animationName, HeroRotateStateData stateData) : base(_upState, hero, animator, animationName, stateData)
    {
    }
  }
}