namespace Services.Random
{
  public interface IRandomService : IService
  {
    void SetSeed(int seed);
    int Next(int min, int max);
    int Next(int max);
    double NextDouble();
  }
}