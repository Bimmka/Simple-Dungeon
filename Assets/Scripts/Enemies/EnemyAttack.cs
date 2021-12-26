using System.Linq;
using Interfaces;
using StaticData.Enemies;
using UnityEngine;

namespace Enemies
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private Transform attackPoint;
        [SerializeField] private EnemyAttackData attackData;
        
        private Collider[] hits;

        private void Awake() => 
            hits = new Collider[attackData.MaxAttackedEntitiesCount];

        public void Attack()
        {
            if (Hit(out Collider hit))
                hit.GetComponentInChildren<IDamageableEntity>().TakeDamage(attackData.Damage, transform.position);
        }

        private bool Hit(out Collider hit)
        {
            int hitAmount = Physics.OverlapSphereNonAlloc(attackPoint.position, attackData.AttackRadius, hits, attackData.Mask);
            hit = hits.FirstOrDefault();
            return hitAmount > 0;
        }

        private void OnDrawGizmosSelected() => 
            Gizmos.DrawWireSphere(attackPoint.position, attackData.AttackRadius);
    }
}
