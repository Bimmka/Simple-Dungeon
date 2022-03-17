using System;
using DG.Tweening;
using UnityEngine;

namespace Hero
{
  public class HeroRotate : MonoBehaviour
  {
    [Range(0,1)]
    [SerializeField] private float lowAngleMoveRotationSpeed = 0.1f;

    [SerializeField] private float speed = 3f;

    private event Action savedCallback;
    public bool IsTurning { get; private set; }

    public void RotateTo(Vector2 moveAxis) => 
      transform.forward = Vector3.Lerp(transform.forward, new Vector3(moveAxis.x, 0, moveAxis.y), lowAngleMoveRotationSpeed);

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

#if DEBUG_MOVE
    public void SetInterpolateValue(float value)
    {
      lowAngleMoveRotationSpeed = value;
    }
#endif

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