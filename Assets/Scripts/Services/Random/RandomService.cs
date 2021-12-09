namespace Services.Random
{
  public class RandomService : IRandomService
  {
    private System.Random random;
    
    public void SetSeed(int seed)
    {
      random = new System.Random(seed);
    }
    
    public int Next(int min, int max) =>
      random.Next(min, max);

    public int Next(int max) => 
      Next(0, max);

    public double NextDouble() => 
      random.NextDouble();
  }
}