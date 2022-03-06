using DG.Tweening;
using UnityEngine;

namespace Hero
{
  public class HeroRotate : MonoBehaviour
  {
    [SerializeField] private float rotateSpeed;
    public bool IsTurning { get; private set; }

    public float RotateDuration => 0.5f;

    public void RotateTo(Vector2 moveAxis) => 
      transform.forward = Vector3.Lerp(transform.forward, new Vector3(moveAxis.x, 0, moveAxis.y), 0.3f);

    public void Rotate() => 
      transform.DORotate(transform.eulerAngles + Vector3.up * (rotateSpeed * Time.deltaTime), Time.deltaTime);

    public void SetIsTurning() => 
      IsTurning = true;

    public void StopRotate()
    {
      DOTween.Kill(transform);
      IsTurning = false;
    }
  }
}