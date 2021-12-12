using GameStates.States.Interfaces;
using Input;
using SceneLoading;
using Services;
using Services.Assets;
using Services.Factories.GameFactories;
using Services.Input;
using Services.Progress;
using Services.Random;
using Services.SaveLoad;
using Services.StaticData;
using Services.UI.Factory;
using Services.UI.Windows;

namespace GameStates.States
{
  public class BootstrapState : IState
  {
    private readonly ISceneLoader sceneLoader;
    private readonly IGameStateMachine gameStateMachine;
    private readonly AllServices services;

    public BootstrapState(IGameStateMachine gameStateMachine, ISceneLoader sceneLoader, ref AllServices services)
    {
      this.gameStateMachine = gameStateMachine;
      this.sceneLoader = sceneLoader;
      this.services = services;
      RegisterServices();
    }

    public void Enter()
    {
      gameStateMachine.Enter<LoadProgressState>();
    }

    public void Exit()
    {
      
    }

    private void RegisterServices()
    {
      RegisterStateMachine();
      RegisterInputService();
      RegisterRandom();
      RegisterProgress();
      RegisterSaveLoad();
      RegisterStaticDataService();
      RegisterAssets();
      RegisterUIFactory();
      RegisterGameFactory();
      RegisterWindowsService();
    }

    private void RegisterGameFactory()
    {
      services.RegisterSingle<IGameFactory>(new GameFactory(
        services.Single<IAssetProvider>(), 
        services.Single<IStaticDataService>(), 
        services.Single<IPersistentProgressService>(),
        services.Single<IWindowsService>(), 
        services.Single<IInputService>()));
    }

    private void RegisterStateMachine() => 
      services.RegisterSingle(gameStateMachine);

    private void RegisterInputService()
    {
      IInputService inputService = new InputService(new HeroControls());
      inputService.Enable();
      services.RegisterSingle<IInputService>(inputService);
    }

    private void RegisterAssets()
    {
      IAssetProvider provider = new AssetProvider();
      services.RegisterSingle(provider);
    }

    private void RegisterStaticDataService()
    {
      IStaticDataService staticData = new StaticDataService();
      staticData.Load();
      services.RegisterSingle(staticData);
    }

    private void RegisterSaveLoad() => 
      services.RegisterSingle(new SaveLoadService(services.Single<IPersistentProgressService>()));

    private void RegisterProgress() => 
      services.RegisterSingle(new PersistentProgressService());

    private void RegisterRandom() => 
      services.RegisterSingle(new RandomService());

    private void RegisterUIFactory() =>
      services.RegisterSingle(new UIFactory(services.Single<IGameStateMachine>(),services.Single<IAssetProvider>(),
        services.Single<IStaticDataService>(), services.Single<IPersistentProgressService>()));

    private void RegisterWindowsService() => 
      services.RegisterSingle(new WindowsService(services.Single<IUIFactory>()));
  }
}