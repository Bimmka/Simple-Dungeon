using System;
using UnityEngine;

namespace Systems.Healths
{
  public class Health : MonoBehaviour, IHealth
  {
    private float maxHealth;
    private float currentHealth;

    public event Action<float, float> Changed;
    public event Action Dead;

    public void SetHp(float current, float max)
    {
      currentHealth = current;
      maxHealth = max;
    }

    public void TakeDamage(float damage)
    {
      currentHealth -= damage;
      Changed?.Invoke(currentHealth, maxHealth);
    }
  }
}
