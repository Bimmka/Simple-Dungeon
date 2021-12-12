using UnityEngine;

namespace Services.Input
{
  public interface IInputService : IService
  {
    void Enable();
    void Disable();
    Vector2 Axis { get; }

    bool IsAttackButtonDown();
    bool IsRollButtonDown();
    bool IsBlockButtonDown();
  }
}