using Services.Input;
using UnityEngine;

namespace Hero
{
  public class HeroInput : MonoBehaviour
  {
    [SerializeField] private HeroStateMachine stateMachine;
    
    private IInputService inputService;

    public void Construct(IInputService inputService)
    {
      this.inputService = inputService;
    }

    private void Update()
    {
      if (inputService.IsAttackButtonDown())
        stateMachine.SetAttackState();

      if (inputService.IsRollButtonDown())
        stateMachine.SetRollState();
      
      stateMachine.SetIsBlocking(inputService.IsBlockButtonPressed());
      stateMachine.SetWalkState(inputService.Axis);
    }
  }
}