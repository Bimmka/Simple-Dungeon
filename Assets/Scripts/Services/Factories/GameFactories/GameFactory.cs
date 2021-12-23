using Hero;
using Services.Assets;
using Services.Input;
using Services.Progress;
using Services.StaticData;
using Services.UI.Windows;
using StaticData.Hero;
using UnityEngine;

namespace Services.Factories.GameFactories
{
  public class GameFactory : IGameFactory
  {
    private readonly IAssetProvider assets;
    private readonly IStaticDataService staticData;
    private readonly IPersistentProgressService progressService;
    private readonly IWindowsService windowsService;
    private readonly IInputService inputService;
    private GameObject heroGameObject;
    
    public GameFactory(IAssetProvider assets, IStaticDataService staticData, IPersistentProgressService progressService, IWindowsService windowsService, IInputService inputService)
    {
      this.assets = assets;
      this.staticData = staticData;
      this.progressService = progressService;
      this.windowsService = windowsService;
      this.inputService = inputService;
    }
    
    public GameObject CreateHero()
    {
      HeroSpawnStaticData spawnData = staticData.ForHero();
      heroGameObject = InstantiateObject(spawnData.HeroPrefab, spawnData.SpawnPoint);
      heroGameObject.GetComponent<HeroInput>().Construct(inputService);
      return heroGameObject;
    }

    public GameObject CreateHud()
    {
      return null;
    }

    private GameObject InstantiateObject(GameObject prefab, Vector3 at) => 
      assets.Instantiate(prefab, at);

    private GameObject InstantiateObject(GameObject prefab) => 
      assets.Instantiate(prefab);

    private GameObject InstantiateObject(GameObject prefab, Transform parent) => 
      assets.Instantiate(prefab, parent);
  }
}