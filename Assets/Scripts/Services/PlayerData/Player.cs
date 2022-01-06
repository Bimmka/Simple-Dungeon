using StaticData.Hero;
using StaticData.Hero.Components;

namespace Services.PlayerData
{
  public class Player
  {
    public readonly Equipment Equipment;
    public readonly Inventory Inventory;
    public readonly PlayerCharacteristics Characteristics;
    public PlayerMoney Monies;
    
    public HeroStaminaStaticData StaminaStaticData { get; private set; }
    public HeroAttackStaticData AttackData { get; private set; }
    public HeroImpactsStaticData ImpactsData { get; private set; }

    public Player(HeroBaseStaticData heroData)
    {
      Characteristics = new PlayerCharacteristics(heroData.Characteristics);
      Equipment = new Equipment(Characteristics, heroData.EquipmentSlots);
      Inventory = new Inventory(heroData.InventorySlotCount);
      Monies = new PlayerMoney();
      StaminaStaticData = heroData.StaminaStaticData;
      AttackData = heroData.AttackData;
      ImpactsData = heroData.ImpactsData;
    }

    public void SetDefaultValue(HeroBaseStaticData heroData)
    {
      Characteristics.SetDefaultValue(heroData.Characteristics);
      Equipment.ReinitSlots(heroData.EquipmentSlots);
      Inventory.ReinitSlots(heroData.InventorySlotCount);
      Monies.RemoveMoney();

      StaminaStaticData = heroData.StaminaStaticData;
      AttackData = heroData.AttackData;
      ImpactsData = heroData.ImpactsData;
    }
  }
}