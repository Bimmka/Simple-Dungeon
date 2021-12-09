using GameStates;
using SceneLoading;
using Services;

namespace Bootstrapp
{
  public class Game
  {
    public readonly GameStateMachine StateMachine;

    public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain, ref AllServices services)
    {
      StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner,curtain), ref services);
    }
  }
}