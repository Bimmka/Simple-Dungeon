using Services.Input;
using UnityEngine;

namespace Hero
{
  public class HeroInput : MonoBehaviour
  {
    [SerializeField] private HeroStateMachine stateMachine;

    private IInputService _inputService;
    private bool isDisabled;

    public void Construct(IInputService inputService)
    {
      _inputService = inputService;
      inputService.Enable();
    }

    private void OnDestroy()
    {
      _inputService.Disable();
    }

    private void Update()
    {
      if (isDisabled)
        return;
      if (_inputService.IsAttackButtonDown())
        stateMachine.SetAttackState();

      if (_inputService.IsRollButtonDown())
        stateMachine.SetRollState();

      stateMachine.SetIsRunning(_inputService.IsRunButtonPressed());

      /* stateMachine.SetIsBlocking(inputService.IsBlockButtonPressed());*/
      stateMachine.SetMoveAxis(_inputService.Axis);
    }

    public void Disable()
    {
      isDisabled = true;
      stateMachine.SetMoveAxis(Vector2.zero);
    }
  }
}