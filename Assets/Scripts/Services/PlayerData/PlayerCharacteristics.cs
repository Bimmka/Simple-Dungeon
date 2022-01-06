using System;
using Loots;

namespace Services.PlayerData
{
  public class PlayerCharacteristics
  {
    private Characteristic[] characteristics;

    public event Action Changed;

    public PlayerCharacteristics(Characteristic[] characteristics) => 
      SetDefaultValue(characteristics);

    public void SetDefaultValue(Characteristic[] heroCharateristics) => 
      characteristics = heroCharateristics;

    public int Stamina()
    {
      for (int i = 0; i < characteristics.Length; i++)
      {
        if (characteristics[i].Type == CharacteristicType.Stamina)
          return characteristics[i].Value;
      }

      return 0;
    }

    public int Damage()
    {
      for (int i = 0; i < characteristics.Length; i++)
      {
        if (characteristics[i].Type == CharacteristicType.Strength)
          return characteristics[i].Value;
      }

      return 0;
    }

    public int Health()
    {
      for (int i = 0; i < characteristics.Length; i++)
      {
        if (characteristics[i].Type == CharacteristicType.Health)
          return characteristics[i].Value;
      }

      return 0;
    }

    public void IncCharacteristic(CharacteristicType type, int value)
    {
      for (int i = 0; i < characteristics.Length; i++)
      {
        if (characteristics[i].Type == type)
        {
          characteristics[i].ChangeValue(value);
          NotifyAboutChange();
          break;
        }
      }
    }

    public void ReduceCharacteristic(CharacteristicType type, int value)
    {
      for (int i = 0; i < characteristics.Length; i++)
      {
        if (characteristics[i].Type == type)
        {
          characteristics[i].ChangeValue(-value);
          NotifyAboutChange();
          break;
        }
      }
    }

    private void NotifyAboutChange() => 
      Changed?.Invoke();
  }
}