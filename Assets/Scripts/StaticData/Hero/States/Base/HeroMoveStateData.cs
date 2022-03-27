using UnityEngine;

namespace StaticData.Hero.States.Base
{
    [CreateAssetMenu(fileName = "HeroMoveStateStaticData", menuName = "Static Data/Hero/States/Create Hero Move State Data", order = 55)]
    public class HeroMoveStateData : HeroBaseStateData
    {
        public AnimationCurve UpStateEnterCurve;
        public AnimationCurve DownStateEnterCurve;
    }
}