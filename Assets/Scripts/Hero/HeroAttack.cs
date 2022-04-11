using Interfaces;
using Services.PlayerData;
using StateMachines.Player.Attack;
using StaticData.Hero.Attacks;
using StaticData.Hero.Components;
using UnityEngine;

namespace Hero
{
  public class HeroAttack : MonoBehaviour
  {
    [SerializeField] private Transform attackPoint;

    private AttacksStaticData attackData;
    private PlayerCharacteristics characteristics;

    private Collider[] hits;

    public void Construct(AttacksStaticData data, PlayerCharacteristics characteristics)
    {
      attackData = data;
      hits = new Collider[attackData.MaxAttackedEntitiesCount];
      this.characteristics = characteristics;
    }

    public void Attack(AttackType attackType)
    {
      for (int i = 0; i < Hit(AttackData(attackType)); i++)
      {
        hits[i].GetComponentInChildren<IDamageableEntity>().TakeDamage(characteristics.Damage(), transform.position);
      }
    }

    private AttackStaticData AttackData(AttackType attackType)
    {
      if (attackData.AttacksData.ContainsKey(attackType))
        return attackData.AttacksData[attackType];
      return new AttackStaticData();
    }

    private int Hit(AttackStaticData data) => 
      Physics.OverlapSphereNonAlloc(attackPoint.position, data.AttackRadius, hits, attackData.Mask);
  }
}