using UnityEngine;
using UnityEngine.UI;

namespace Debugging
{
  public class ExitEscape : MonoBehaviour
  {
    [SerializeField] private Button exitButton;

    private void Awake()
    {
      exitButton.onClick.AddListener(Exit);
    }

    private void OnDestroy()
    {
      exitButton.onClick.RemoveListener(Exit);
    }

    private void Exit()
    {
      Application.Quit();
    }
  }
}