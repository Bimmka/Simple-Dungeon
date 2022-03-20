using StaticData.Hero.States;

namespace StateMachines.Player
{
  public class HeroBaseLowMachineState
  {
    protected readonly HeroStateData stateData;
    
    public int Weight => stateData.Weight;
  }
}