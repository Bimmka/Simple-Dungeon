using System;
using GameStates;
using GameStates.States;
using SceneLoading;
using Services;
using UnityEngine;

namespace Bootstrapp
{
  public class GameBootstrapp : MonoBehaviour, ICoroutineRunner
  {
    [SerializeField] private LoadingCurtain curtainPrefab;
    private Game game;
    private AllServices allServices;

    private void Awake()
    {
      allServices = new AllServices();
      game = new Game(this, Instantiate(curtainPrefab), ref allServices);
      game.StateMachine.Enter<BootstrapState>();

      DontDestroyOnLoad(this);
    }

    private void OnDestroy()
    {
      allServices.Cleanup();
    }
  }
}