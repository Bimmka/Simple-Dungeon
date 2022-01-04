using System;
using ConstantsValue;
using GameStates;
using Services.Assets;
using Services.Progress;
using Services.StaticData;
using StaticData.UI;
using UI.Base;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Services.UI.Factory
{
  public class UIFactory : IUIFactory
  {
    private readonly IGameStateMachine gameStateMachine;
    private readonly IAssetProvider assets;
    private readonly IStaticDataService staticData;
    private readonly IPersistentProgressService progressService;

    private Transform uiRoot;

    private Camera mainCamera;

    public event Action<WindowId,BaseWindow> Spawned;

    public UIFactory(IGameStateMachine gameStateMachine, IAssetProvider assets, IStaticDataService staticData, IPersistentProgressService progressService)
    {
      this.gameStateMachine = gameStateMachine;
      this.assets = assets;
      this.staticData = staticData;
      this.progressService = progressService;
    }

    public void CreateUIRoot()
    {
      uiRoot = assets.Instantiate<GameObject>(AssetsPath.UIRootPath).transform;
      uiRoot.GetComponent<UIRoot>().SetCamera(GetCamera());
    }

    public void CreateWindow(WindowId id)
    {
      WindowInstantiateData config = LoadWindowInstantiateData(id);
      switch (id)
      {
        default:
          CreateWindow(config, id);
          break;
      }
    }

    private void CreateWindow(WindowInstantiateData config, WindowId id)
    {
      BaseWindow window = InstantiateWindow(config);
      NotifyAboutCreateWindow(id, window);
    }

    private void NotifyAboutCreateWindow(WindowId id, BaseWindow window) => 
      Spawned?.Invoke(id, window);

    private BaseWindow InstantiateWindow(WindowInstantiateData config) => 
      assets.Instantiate(config.Window, uiRoot);

    private WindowInstantiateData LoadWindowInstantiateData(WindowId id) => 
      staticData.ForWindow(id);

    private Camera GetCamera()
    {
      if (mainCamera == null)
        mainCamera = Camera.main;
      return mainCamera;
    }
  }
}