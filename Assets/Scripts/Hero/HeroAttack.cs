using Interfaces;
using StaticData.Hero.Components;
using UnityEngine;

namespace Hero
{
  public class HeroAttack : MonoBehaviour
  {
    [SerializeField] private Transform attackPoint;

    private HeroAttackStaticData attackData;

    private Collider[] hits;

    public void Construct(HeroAttackStaticData data)
    {
      attackData = data;
      hits = new Collider[attackData.MaxAttackedEntitiesCount];
    }

    public void Attack()
    {
      for (int i = 0; i < Hit(); i++)
      {
        hits[i].GetComponentInChildren<IDamageableEntity>().TakeDamage(attackData.Damage, transform.position);
      }
    }

    private int Hit() => 
      Physics.OverlapSphereNonAlloc(attackPoint.position, attackData.AttackRadius, hits, attackData.Mask);

    private void OnDrawGizmosSelected() => 
      Gizmos.DrawWireSphere(attackPoint.position, attackData.AttackRadius);
  }
}