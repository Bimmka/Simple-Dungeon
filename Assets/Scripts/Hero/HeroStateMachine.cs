using System;
using StateMachines;
using UnityEngine;

namespace Hero
{
    public class HeroStateMachine : MonoBehaviour
    {
        private StateMachine stateMachine;

        private void Awake()
        {
            CreateStateMachine();
            CreateStates();
            SetDefaultState();
        }

        private void CreateStateMachine() => 
            stateMachine = new StateMachine();

        private void CreateStates()
        {
            
        }

        private void SetDefaultState()
        {
            
        }
    }
}
