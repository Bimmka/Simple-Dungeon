using UnityEngine;

namespace StaticData.UI.Statistic
{
  [CreateAssetMenu(fileName = "Statistic Displayer Data", menuName = "Static Data/UI/Create Statistic Displayer Data", order = 55)]
  public class StatisticDisplayerStaticData : ScriptableObject
  {
    public Color WinColor;
    public Color LoseColor;
    public Color DrawColor;
  }
}