using Services.UI.Factory;
using StaticData.UI;

namespace Services.StaticData
{
  public interface IStaticDataService : IService
  {
    void Load();
    WindowInstantiateData ForWindow(WindowId id);
  }
}