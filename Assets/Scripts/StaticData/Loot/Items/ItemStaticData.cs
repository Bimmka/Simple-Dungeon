using Loots;
using UnityEngine;

namespace StaticData.Loot.Items
{
  [CreateAssetMenu(fileName = "ItemStaticData", menuName = "Static Data/Loot/Items/Create Item Data", order = 55)]
  public class ItemStaticData : ScriptableObject
  {
    public string Name;
    public string Description;
    public Sprite Icon;
    public LootRareType Rarity;
    public LootType Type;
    public Characteristic[] Characteristics;
    public GameObject Prefab;
  }
}