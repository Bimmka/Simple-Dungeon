using GameStates;
using UI.Base;

namespace UI.Windows.Menus
{
  public class PauseMenuWindow : BaseWindow
  {
    private IGameStateMachine gameStateMachine;

    public void Construct(IGameStateMachine gameStateMachine)
    {
      this.gameStateMachine = gameStateMachine;
    }
  }
}