using System;
using Interfaces;
using StaticData.Bonuses;
using UnityEngine;

namespace Bonuses
{
  [RequireComponent(typeof(BoxCollider))]
  public class Bonus : MonoBehaviour, IPickedupObject<Bonus>
  {
    private BonusUseStrategy useStrategy;
    private int value;

    private GameObject view;
    
    public BonusTypeId Type { get; private set; }

    public event Action<Bonus> PickedUp;

    public void Show() => 
      gameObject.SetActive(true);

    public void Hide() => 
      gameObject.SetActive(false);

    public void SetPosition(Vector3 position) => 
      transform.position = position;

    private void OnTriggerEnter(Collider other)
    {
      if (useStrategy.IsCanBePickedUp(other))
      {
        useStrategy.Pickup(other, value);
        NotifyAboutPickedup();
      }
    }

    protected void NotifyAboutPickedup() => 
      PickedUp?.Invoke(this);
  }
}