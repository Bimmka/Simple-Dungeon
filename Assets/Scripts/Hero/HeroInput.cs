using Services.Input;
using UnityEngine;

namespace Hero
{
  public class HeroInput : MonoBehaviour
  {
    [SerializeField] private HeroStateMachine stateMachine;

    private IInputService _inputService;
    private bool _isDisabled;

    private Camera _mainCamera;
    private readonly RaycastHit[] hits = new RaycastHit[1];

    public void Construct(IInputService inputService)
    {
      _inputService = inputService;
      inputService.Enable();
      _mainCamera = Camera.main;
    }

    private void OnDestroy()
    {
      _inputService.Disable();
    }

    private void Update()
    {
      if (_isDisabled)
        return;
      if (_inputService.IsAttackButtonDown()) 
        stateMachine.SetAttackState(ClickPoint());

      if (_inputService.IsRollButtonDown())
        stateMachine.SetRollState();

      stateMachine.SetIsRunning(_inputService.IsRunButtonPressed());

      stateMachine.SetIsBlocking(_inputService.IsBlockButtonPressed());
      stateMachine.SetMoveAxis(new Vector2(1,0));
    }

    public void Disable()
    {
      _isDisabled = true;
      stateMachine.SetMoveAxis(Vector2.zero);
    }
    
    private Vector3 ClickPoint()
    {
      Ray ray = _mainCamera.ScreenPointToRay(_inputService.ClickPosition);
      Physics.RaycastNonAlloc(ray, hits);
      return hits[0].point;
    }
  }
}