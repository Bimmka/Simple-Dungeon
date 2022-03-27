using StaticData.Hero.Components;
using UnityEngine;

namespace Hero
{
  public class HeroMove : MonoBehaviour
  {
    [SerializeField] private CharacterController characterController;
    [SerializeField] private HeroMoveStaticData moveData;

    public void Move(Vector3 direction)
    {
      direction.y = moveData.Gravity;
      characterController.Move(direction * (moveData.MoveSpeed * Time.deltaTime));
    }

    public void Run(Vector3 direction)
    {
      direction.y = moveData.Gravity;
      characterController.Move(direction * (moveData.RunSpeed * Time.deltaTime));
    }

    public void Roll()
    {
      Vector3 direction = new Vector3(transform.forward.x, moveData.Gravity, transform.forward.z);
      characterController.Move(direction * (moveData.RollSpeed * Time.deltaTime));
    }
  }
}