﻿using System.Collections;
using Services;
using StateMachines.Player.Attack;
using StaticData.Hero.Attacks.Combo;
using UnityEngine;

namespace Hero
{
  public class HeroAttacksCombo
  {
    private readonly HeroCombosStaticData _combosData;
    private readonly ICoroutineRunner _coroutineRunner;

    private ComboStep _previousComboStep;
    private ComboStep _nextComboStep;
    private int _previousAttackStepIndex;
    private float _previousAttackFinishTime;

    private Coroutine _lifeTimeCoroutine;
     

    public HeroAttacksCombo(HeroCombosStaticData combos, ICoroutineRunner coroutineRunner)
    {
      _combosData = combos;
      _coroutineRunner = coroutineRunner;
    }

    public AttackType NextAttack()
    {
      for (int i = 0; i < _combosData.Combos.Count; i++)
      {
        if (_combosData.Combos[i].Steps.Count > _previousAttackStepIndex && IsProperStep(_combosData.Combos[i].Steps[_previousAttackStepIndex]))
        {
          _nextComboStep = _combosData.Combos[i].Steps[_previousAttackStepIndex];
          return _nextComboStep.NextAttack;
        }
      }

      return AttackType.None;
    }

    public void StartAttack()
    {
      if (_lifeTimeCoroutine != null)
        _coroutineRunner.StopCoroutine(_lifeTimeCoroutine);
    }

    public void ApplyAttack()
    {
      _previousComboStep = _nextComboStep;
      _previousAttackStepIndex++;
    }

    public void AttackFinished(StateFinishType stateFinishType)
    {
      _previousAttackFinishTime = Time.time;
      switch (stateFinishType)
      {
        case StateFinishType.Canceling:
          break;
        case StateFinishType.Interrupted:
        case StateFinishType.Finish:
          ApplyAttack();
          break;
      }
      _lifeTimeCoroutine = _coroutineRunner.StartCoroutine(CountdownComboStepLifeTime(_previousComboStep.LifeTime));
    }

    private IEnumerator CountdownComboStepLifeTime(float lifeTime)
    {
      yield return new WaitForSeconds(lifeTime);
      ResetCombo();
      _lifeTimeCoroutine = null;
    }

    private void ResetCombo()
    {
      _previousComboStep = _combosData.EmptyStep;
      _previousAttackStepIndex = 0;
    }

    private bool IsProperStep(ComboStep step) => 
      step.PreviousAttack == _previousComboStep.NextAttack && step.Delay <= Time.time - _previousAttackFinishTime;
  }
}