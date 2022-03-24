using Animations;
using Hero;
using StaticData.Hero.States;
using UnityEngine;

namespace StateMachines.Player
{
  public abstract class HeroRotatingSubState : HeroBaseSubStateMachineState<HeroRotatingUpMachineState, HeroRotatingSubState>
  {
    public AnimationCurve EnterCurve => stateData.EnterCurve;
    public AnimationCurve ExitCurve => stateData.ExitCurve;

    public HeroRotatingSubState(HeroRotatingUpMachineState _upState, HeroStateMachine hero, BattleAnimator animator, string animationName, HeroStateData stateData) : base(_upState, hero, animator, animationName, stateData)
    {
    }
  }
}