using System;
using InputActions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Services.Input
{
  public class InputService : IInputService
  {
    private readonly HeroControls input;
    private Mouse currentMouse;

    public Vector2 Axis => 
      SimpleInputAxis();

    public Vector2 ClickPosition => 
      MousePosition();

    public Guid AttackActionGuid => input.Player.Attack.id;
    public Guid MoveActionGuid => input.Player.Move.id;
    public Guid RollActionGuid => input.Player.Roll.id;
    public Guid SpecialActionGuid => input.Player.SpecialAction.id;
    public Guid RunActionGuid => input.Player.Run.id;


    public InputService(HeroControls inputMap)
    {
      input = inputMap;
    }

    public void Enable()
    {
      input.Enable();
      currentMouse = Mouse.current;
    }

    public void Disable() => 
      input.Disable();

    public bool IsAttackButtonDown() => 
      input.Player.Attack.triggered;

    public bool IsRollButtonDown() => 
      input.Player.Roll.triggered;

    public bool IsBlockButtonPressed() => 
      input.Player.SpecialAction.IsPressed();

    public bool IsRunButtonPressed() => 
      input.Player.Run.IsPressed();

    private Vector2 SimpleInputAxis() => 
      input.Player.Move.ReadValue<Vector2>();

    private Vector2 MousePosition() => 
      input.Player.Mouse.ReadValue<Vector2>();
  }
}