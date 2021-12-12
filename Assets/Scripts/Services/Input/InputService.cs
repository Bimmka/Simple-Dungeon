using Input;
using UnityEngine;

namespace Services.Input
{
  public class InputService : IInputService
  {
    private readonly HeroControls input;

    public InputService(HeroControls inputMap)
    {
      input = inputMap;
    }

    public virtual void Enable() => 
      input.Enable();

    public virtual void Disable() => 
      input.Disable();

    public Vector2 Axis => 
      SimpleInputAxis();

    public bool IsAttackButtonDown() => 
      input.Player.Attack.triggered;

    public bool IsRollButtonDown() => 
      input.Player.Roll.triggered;

    public bool IsBlockButtonDown() => 
      input.Player.Block.triggered;

    protected Vector2 SimpleInputAxis() => 
      input.Player.Move.ReadValue<Vector2>();
  }
}