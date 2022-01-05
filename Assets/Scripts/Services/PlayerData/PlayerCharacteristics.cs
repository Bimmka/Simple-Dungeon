using System;
using Loots;

namespace Services.PlayerData
{
  public class PlayerCharacteristics
  {
    private readonly Characteristic[] Characteristics;

    public event Action Changed;

    public PlayerCharacteristics(Characteristic[] characteristics)
    {
      Characteristics = characteristics;
    }

    public int Stamina()
    {
      for (int i = 0; i < Characteristics.Length; i++)
      {
        if (Characteristics[i].Type == CharacteristicType.Stamina)
          return Characteristics[i].Value;
      }

      return 0;
    }

    public int Damage()
    {
      for (int i = 0; i < Characteristics.Length; i++)
      {
        if (Characteristics[i].Type == CharacteristicType.Strength)
          return Characteristics[i].Value;
      }

      return 0;
    }

    public int Health()
    {
      for (int i = 0; i < Characteristics.Length; i++)
      {
        if (Characteristics[i].Type == CharacteristicType.Health)
          return Characteristics[i].Value;
      }

      return 0;
    }

    public void IncCharacteristic(CharacteristicType type, int value)
    {
      for (int i = 0; i < Characteristics.Length; i++)
      {
        if (Characteristics[i].Type == type)
        {
          Characteristics[i].ChangeValue(value);
          NotifyAboutChange();
          break;
        }
      }
    }

    public void ReduceCharacteristic(CharacteristicType type, int value)
    {
      for (int i = 0; i < Characteristics.Length; i++)
      {
        if (Characteristics[i].Type == type)
        {
          Characteristics[i].ChangeValue(-value);
          NotifyAboutChange();
          break;
        }
      }
    }

    private void NotifyAboutChange() => 
      Changed?.Invoke();
  }
}