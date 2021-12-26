using UnityEngine;

namespace StaticData.Enemies
{
  [CreateAssetMenu(fileName = "EnemyAttackData", menuName = "Static Data/Enemies/Create Enemy Attack Data", order = 55)]
  public class EnemyAttackData : ScriptableObject
  {
    public float Damage;
    public float AttackCooldown;
    public float AttackRadius;
    public LayerMask Mask;
    public int MaxAttackedEntitiesCount;
  }
}