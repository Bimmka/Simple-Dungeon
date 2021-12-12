using System.Collections.Generic;
using System.Linq;
using ConstantsValue;
using Services.UI.Factory;
using StaticData.Hero;
using StaticData.UI;
using UnityEngine;

namespace Services.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private Dictionary<WindowId, WindowInstantiateData> windows;
    private HeroSpawnStaticData heroData;
    public void Load()
    {
      heroData = Resources.Load<HeroSpawnStaticData>(AssetsPath.HeroDataPath);
    }
    
    public WindowInstantiateData ForWindow(WindowId windowId) =>
      windows.TryGetValue(windowId, out WindowInstantiateData staticData)
        ? staticData 
        : new WindowInstantiateData();

    public HeroSpawnStaticData ForHero() => 
      heroData;
  }
}