using System;
using Services.Input;
using UnityEngine;

namespace Hero
{
  public class HeroInput : MonoBehaviour
  {
    [SerializeField] private HeroStateMachine stateMachine;
    
    private IInputService inputService;
    private bool isDisabled;

    public void Construct(IInputService inputService)
    {
      this.inputService = inputService;
      inputService.Enable();
    }

    private void OnDestroy()
    {
      inputService.Disable();
    }

    private void Update()
    {
      if (isDisabled)
        return;      
     /* if (inputService.IsAttackButtonDown())
        stateMachine.SetAttackState();*/

     /* if (inputService.IsRollButtonDown())
        stateMachine.SetRollState();*/
     
       stateMachine.SetIsRunning(inputService.IsRunButtonPressed());
      
     /* stateMachine.SetIsBlocking(inputService.IsBlockButtonPressed());*/
      stateMachine.SetMoveAxis(inputService.Axis);
    }

    public void Disable()
    {
      isDisabled = true;
      stateMachine.SetMoveAxis(Vector2.zero);
    }
  }
}