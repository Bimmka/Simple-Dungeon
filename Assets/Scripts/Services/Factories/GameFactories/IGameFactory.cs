using UnityEngine;

namespace Services.Factories.GameFactories
{
  public interface IGameFactory : IService
  {
    GameObject CreateHero();
    GameObject CreateHud();
  }
}