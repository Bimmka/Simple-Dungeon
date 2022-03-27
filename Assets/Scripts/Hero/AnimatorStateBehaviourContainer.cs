using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hero
{
  public class AnimatorStateBehaviourContainer : MonoBehaviour
  {
    [SerializeField] private Animator _animator;

    private Dictionary<Type, StateMachineBehaviour> _subStatesBehaviour;


    public void Initialize()
    {
      FillBehaviours();
    }

    public TBehaviour GetStateBehaviour<TBehaviour>() where TBehaviour : StateMachineBehaviour
    {
      if (_subStatesBehaviour.ContainsKey(typeof(TBehaviour)))
        return (TBehaviour) _subStatesBehaviour[typeof(TBehaviour)];
      return null;
    }

    private void FillBehaviours()
    {
      StateMachineBehaviour[] behaviours = _animator.GetBehaviours<StateMachineBehaviour>();
      _subStatesBehaviour = new Dictionary<Type, StateMachineBehaviour>(behaviours.Length);
      for (int i = 0; i < behaviours.Length; i++)
      {
        _subStatesBehaviour.Add(behaviours[i].GetType(), behaviours[i]);
      }
    }
  }
}