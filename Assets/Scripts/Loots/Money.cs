using System;
using Hero;
using Interfaces;
using UnityEngine;

namespace Loots
{
  [RequireComponent(typeof(BoxCollider))]
  public class Money : MonoBehaviour, IPickedupObject<Money>
  {
    public event Action<Money> PickedUp;

    public void Hide() => 
      gameObject.SetActive(false);

    public void SetPosition(Vector3 position) => 
      transform.position = position;

    public void Show() => 
      gameObject.SetActive(true);

    private void OnTriggerEnter(Collider other)
    {
      if (other.TryGetComponent(out HeroMoney heroMoney))
        Pickup(heroMoney);
    }

    private void Pickup(HeroMoney heroMoney)
    {
      heroMoney.AddMoney(1);
      PickedUp?.Invoke(this);
    }
  }
}