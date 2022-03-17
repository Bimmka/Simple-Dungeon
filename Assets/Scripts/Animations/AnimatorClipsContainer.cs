using System;
using System.Collections.Generic;
using System.Linq;
using StateMachines.Player;
using UnityEngine;

namespace Animations
{
  public class AnimatorClipsContainer : MonoBehaviour
  {
    [SerializeField] private Animator _animator;

    private Dictionary<PlayerActionsType, AnimationClip> _clips;

    public void CollectClips()
    {
      AnimationClip[] clips = _animator.runtimeAnimatorController.animationClips.Distinct().ToArray();
      _clips = new Dictionary<PlayerActionsType, AnimationClip>(clips.Length);
      
      PlayerActionsType type;
      for (int i = 0; i < clips.Length; i++)
      {
        Enum.TryParse(clips[i].name, out type);
        _clips.Add(type, clips[i]);
      }
    }

    public float ClipLength(PlayerActionsType actionType)
    {
      if (_clips.ContainsKey(actionType))
        return _clips[actionType].length;
      return 0f;
    }
  }
}
