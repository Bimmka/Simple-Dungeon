using StaticData.Hero.Components;
using UnityEngine;

namespace Hero
{
  public class HeroMove : MonoBehaviour
  {
    [SerializeField] private CharacterController characterController;
    [SerializeField] private HeroMoveStaticData moveData;

    public void Move(Vector3 direction) => 
      characterController.Move(direction * moveData.MoveSpeed * Time.deltaTime);

    public void Roll() => 
      characterController.Move(transform.forward * moveData.RollSpeed * Time.deltaTime);
  }
}