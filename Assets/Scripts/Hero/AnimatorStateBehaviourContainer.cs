using System;
using System.Collections.Generic;
using StateMachines.Player.AnimationStatesBehaviour;
using UnityEngine;

namespace Hero
{
  public class AnimatorStateBehaviourContainer : MonoBehaviour
  {
    [SerializeField] private Animator _animator;

    private Dictionary<Type, BaseStateBehaviour> _subStatesBehaviour;


    public void Initialize()
    {
      FillBehaviours();
    }

    public TBehaviour GetStateBehaviour<TBehaviour>() where TBehaviour : BaseStateBehaviour
    {
      if (_subStatesBehaviour.ContainsKey(typeof(TBehaviour)))
        return (TBehaviour) _subStatesBehaviour[typeof(TBehaviour)];
      return null;
    }

    private void FillBehaviours()
    {
      BaseStateBehaviour[] behaviours = _animator.GetBehaviours<BaseStateBehaviour>();
      _subStatesBehaviour = new Dictionary<Type, BaseStateBehaviour>(behaviours.Length);
      for (int i = 0; i < behaviours.Length; i++)
      {
        _subStatesBehaviour.Add(behaviours[i].GetType(), behaviours[i]);
      }
    }
  }
}