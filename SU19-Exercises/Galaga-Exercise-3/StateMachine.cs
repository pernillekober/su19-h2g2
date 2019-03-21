using System;
<<<<<<< HEAD
=======
using System.Windows.Forms;
>>>>>>> 2fff06a0ce89217e5c5d16413f0d9fa45799176b
using DIKUArcade.EventBus;
using DIKUArcade.State;
using Galaga_Exercise_3.GalagaGame;
using Galaga_Exercise_3.GalagaStateType;
using GalagaGame.GalagaState;

namespace GalagaGame.GalagaState {
    public class StateMachine : IGameEventProcessor<object> {
        public IGameState ActiveState { get; private set; }

        public StateMachine() {
            GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);

            ActiveState = MainMenu.GetInstance();
        }

        private void SwitchState(GameStateType stateType) {
            switch (stateType) {
               case (GameStateType.GameRunning):
                   ActiveState = GameRunning.GetInstance();
                   break;
               case (GameStateType.GamePaused):
                   ActiveState = GamePaused.GetInstance;
                   break;
               case (GameStateType.MainMenu):
                   ActiveState = MainMenu.Getinstance();
                   break;
            }
        }
        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
            if (eventType == GameEventType.GameStateEvent) {
                switch (gameEvent.Message) {
                case "MAIN_MENU":
                    SwitchState(GameStateType.MainMenu);
                    break;
                case "GAME_RUNNING":
                    SwitchState(GameStateType.GameRunning);
                    break;
                case "GAME_PAUSED":
                    SwitchState(GameStateType.GamePaused);
                    break;
                }
            }    
        }
    }
}