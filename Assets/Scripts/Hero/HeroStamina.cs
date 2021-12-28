using System;
using System.Collections;
using Systems.Healths;
using StaticData.Hero.Components;
using UnityEngine;

namespace Hero
{
  public class HeroStamina : MonoBehaviour, IStamina
  {
    [SerializeField] private HeroStaminaStaticData staminaData;

    private float currentStamina;
    private Coroutine recoveryCoroutine;

    public event Action<float, float> Changed; 

    private void Awake()
    {
      SetDefaultValue();
    }

    public bool IsCanAttack() => 
      currentStamina - staminaData.AttackCost >= 0;

    public bool IsCanRoll() => 
      currentStamina - staminaData.RollCost >= 0;

    public void WasteToAttack()
    {
      currentStamina -= staminaData.AttackCost;
      if (recoveryCoroutine == null)
        recoveryCoroutine = StartCoroutine(RecoveryValue());
      Display();
    }

    public void WasteToRoll()
    {
      currentStamina -= staminaData.RollCost;
      if (recoveryCoroutine == null)
        recoveryCoroutine = StartCoroutine(RecoveryValue());
      Display();
    }

    private void SetDefaultValue() => 
      currentStamina = staminaData.MaxStamina;

    private void Display() => 
      Changed?.Invoke(currentStamina, staminaData.MaxStamina);

    private IEnumerator RecoveryValue()
    {
      while (currentStamina < staminaData.MaxStamina)
      {
        yield return new WaitForSeconds(staminaData.RecoveryRate);
        currentStamina += staminaData.RecoveryCount;
        Display();
      }

      recoveryCoroutine = null;
    }
  }
}