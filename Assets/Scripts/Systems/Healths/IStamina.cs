namespace Systems.Healths
{
  public interface IStamina : IChangedValue
  {
    bool IsCanAttack(int attackCost);
    bool IsCanRoll();
    void WasteToAttack(int attackCost);
    void WasteToRoll();
  }
}