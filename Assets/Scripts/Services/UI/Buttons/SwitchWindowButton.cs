using Services.UI.Factory;
using UnityEngine;

namespace UI.Buttons
{
  public class SwitchWindowButton : OpenWindowButton
  {
    [SerializeField] private WindowId closeWindowId;

    protected override void Open()
    {
      windowsService.Close(closeWindowId);
      base.Open();
    }
  }
}