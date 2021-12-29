using StaticData.Level;

namespace Services.Waves
{
  public interface IWaveServices : IService
  {
    void Start();
    void SetLevelWaves(LevelWaveStaticData wavesData);
  }
}