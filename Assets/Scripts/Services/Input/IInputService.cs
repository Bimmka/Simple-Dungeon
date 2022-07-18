using System;
using UnityEngine;

namespace Services.Input
{
  public interface IInputService : IService
  {
    Vector2 Axis { get; }
    Vector2 ClickPosition { get; }
    Guid AttackActionGuid { get; }
    Guid MoveActionGuid { get; }
    Guid RollActionGuid { get; }
    Guid SpecialActionGuid { get; }
    Guid RunActionGuid { get; }

    void Enable();
    void Disable();
    bool IsAttackButtonDown();
    bool IsRollButtonDown();
    bool IsBlockButtonPressed();
    bool IsRunButtonPressed();
  }
}