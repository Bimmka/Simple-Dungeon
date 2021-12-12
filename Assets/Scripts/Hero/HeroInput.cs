using Services.Input;
using UnityEngine;

namespace Hero
{
  public class HeroInput : MonoBehaviour
  {
    private IInputService inputService;

    public void Construct(IInputService inputService)
    {
      this.inputService = inputService;
    }

    private void Update()
    {
      if (inputService.IsAttackButtonDown())
        Debug.Log("Attack");
      if (inputService.IsBlockButtonDown())
        Debug.Log("Block");
      if (inputService.IsRollButtonDown())
        Debug.Log("Roll");
            
      Debug.Log($"Move {inputService.Axis}");
    }
  }
}