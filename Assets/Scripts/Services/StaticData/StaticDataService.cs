using System.Collections.Generic;
using System.Linq;
using ConstantsValue;
using Services.UI.Factory;
using StaticData.UI;
using UnityEngine;

namespace Services.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private Dictionary<WindowId, WindowInstantiateData> windows;
    public void Load()
    {
      windows = Resources
        .Load<WindowsStaticData>(AssetsPath.WindowsDataPath)
        .InstantiateData
        .ToDictionary(x => x.ID, x => x);
    }
    
    public WindowInstantiateData ForWindow(WindowId windowId) =>
      windows.TryGetValue(windowId, out WindowInstantiateData staticData)
        ? staticData 
        : new WindowInstantiateData();
  }
}