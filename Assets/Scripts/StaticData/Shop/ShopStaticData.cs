using StaticData.Loot.Items;
using UnityEngine;

namespace StaticData.Shop
{
  [CreateAssetMenu(fileName = "ShopStaticData", menuName = "Static Data/Shop/Create Shop Data", order = 55)]
  public class ShopStaticData : ScriptableObject
  {
    public ItemStaticData[] Items;
    public Vector2Int ItemCountRange;
  }
}