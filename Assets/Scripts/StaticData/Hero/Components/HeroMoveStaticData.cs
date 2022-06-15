using UnityEngine;

namespace StaticData.Hero.Components
{
  [CreateAssetMenu(fileName = "Hero Move Static Data", menuName = "Static Data/Hero/Create Hero Move Data", order = 55)]
  public class HeroMoveStaticData : ScriptableObject
  {
    public float MoveSpeed = 3f;
    public float RunSpeed = 5f;
    public float ShieldMoveSpeed = 1.5f;
    public float ShieldRunMoveSpeed = 2.5f;
    public float RollSpeed = 2f;
    [Range(-1f,1f)]
    public float TurnAroundTriggerValue = -0.8f;
    public float Gravity = 1.5f;
  }
}