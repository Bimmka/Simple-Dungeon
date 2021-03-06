using System;
using DG.Tweening;
using UnityEngine;

namespace Hero
{
  public class HeroRotate : MonoBehaviour
  {
    [Range(0,1)]
    [SerializeField] private float lowAngleMoveRotationSpeed = 0.1f;
    private event Action savedCallback;
    public bool IsTurning { get; private set; }

    public void RotateTo(Vector2 moveAxis) => 
      transform.forward = Vector3.Lerp(transform.forward, new Vector3(moveAxis.x, 0, moveAxis.y), lowAngleMoveRotationSpeed);
    
    public void ForceRotateTo(Vector3 to)
    {
      Vector3 directionToClick = to - transform.position;
      directionToClick.y = 0;
      directionToClick = directionToClick.normalized;

     transform.forward = Vector3.Lerp(transform.forward, directionToClick, 1f);
    }

    public void TurnAround(Vector3 endDirection, float duration, Action callback)
    {
      Rotate(endDirection, duration, OnTurnAround);
      savedCallback = callback;
    }

    public void Turn(Vector3 endDirection, float duration, Action callback)
    {
      Rotate(endDirection, duration, OnTurn);
      savedCallback = callback;
    }

    public void SetIsTurning() => 
      IsTurning = true;

    private void Rotate(Vector3 endDirection, float length, Action callback = null)
    {
      float angle = Vector3.SignedAngle(transform.forward, endDirection, Vector3.up);
      transform.DORotate(transform.eulerAngles + Vector3.up * angle, length).OnComplete(() => callback?.Invoke());
    }

    private void OnTurn()
    {
      IsTurning = false;
      savedCallback?.Invoke();
    }

    private void OnTurnAround()
    {
      IsTurning = false;
      savedCallback?.Invoke();
    }

    public void StopRotate()
    {
      DOTween.Kill(transform);
      IsTurning = false;
    }
  }
}