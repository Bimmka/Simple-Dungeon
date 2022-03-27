using UnityEngine;

namespace StaticData.Hero.States.Base
{
    [CreateAssetMenu(fileName = "HeroRotateStateStaticData", menuName = "Static Data/Hero/States/Create Hero Rotate State Data", order = 55)]
    public class HeroRotateStateData : HeroBaseStateData
    {
        public AnimationCurve LeftTurnEnterCurve;
        public AnimationCurve LeftTurnExitCurve;
        public AnimationCurve RightTurnEnterCurve;
        public AnimationCurve RightTurnExitCurve;
        public AnimationCurve TurnAroundEnterCurve;
        public AnimationCurve TurnAroundExitCurve;
    }
}