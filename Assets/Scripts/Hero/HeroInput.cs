using System;
using Services.Input;
using UnityEngine;

namespace Hero
{
  public class HeroInput : MonoBehaviour
  {
    [SerializeField] private HeroStateMachine stateMachine;
    
    private IInputService inputService;
    private Camera mainCamera;
    private readonly RaycastHit[] hits = new RaycastHit[1];

    public void Construct(IInputService inputService)
    {
      this.inputService = inputService;
    }

    private void Start()
    {
      mainCamera = Camera.main;
    }

    private void Update()
    {
      if (inputService.IsAttackButtonDown())
        stateMachine.SetAttackState();

      if (inputService.IsRollButtonDown())
        stateMachine.SetRollState();
      
      stateMachine.SetIsBlocking(inputService.IsBlockButtonPressed());
      stateMachine.SetMoveAxis(inputService.Axis);
      stateMachine.SetRotate(Rotation());
    }

    private float Rotation()
    {
      Ray ray = mainCamera.ScreenPointToRay(inputService.ClickPosition);
      if (Physics.RaycastNonAlloc(ray, hits) > 0)
        return Angle(hits[0].point);

      return 0;
    }

    private float Angle(Vector3 mouseClick)
    {
      Vector2 differenceDirection = new Vector2(mouseClick.x - transform.position.x, mouseClick.z - transform.position.z).normalized;
      Vector2 forward = new Vector2(transform.forward.x, transform.forward.z);
      return Vector2.SignedAngle(differenceDirection, forward);
    }
  }
}