using System;
using Interfaces;
using UnityEngine;

namespace Systems.Healths
{
  public class Health : MonoBehaviour
  {
    [SerializeField] private float maxHealth;

    private float currentHealth;

    public event Action Damaged;
    public event Action Dead;

    private void Awake()
    {
      currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
      currentHealth -= damage;
      Damaged?.Invoke();
    }
  }
}
