using Services.PlayerData;
using StaticData.Hero;
using StaticData.Hero.Components;

namespace Services.Progress
{
  public class PersistentProgressService : IPersistentProgressService
  {
    private readonly HeroBaseStaticData baseStaticData;
    public Player Player { get; private set; }
    public PersistentProgressService(HeroBaseStaticData baseStaticData)
    {
      Player = new Player(baseStaticData);
      this.baseStaticData = baseStaticData;
    }

    public void SetPlayerToDefault() => 
      Player = new Player(baseStaticData);
  }
}