using System;
using DIKUArcade.EventBus;
using DIKUArcade.State;
using Galaga_Exercise_3.GalagaGame;
using Galaga_Exercise_3.GalagaStateType;
using Galaga_Exercise_3.GalagaStates;

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
                   ActiveState = GamePaused.GetInstance();
                   break;
               case (GameStateType.MainMenu):
                   ActiveState = MainMenu.GetInstance();
                   break;
            }
        }
        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
            if (eventType == GameEventType.GameStateEvent && gameEvent.Message == "CHANGE_STATE") {
                SwitchState(StateTransformer.TransformStringToState(gameEvent.Parameter1));
                /*
                switch (gameEvent.Parameter1) {
                case "MAIN_MENU":
                    SwitchState(GameStateType.MainMenu);
                    break;
                case "GAME_RUNNING":
                    SwitchState(GameStateType.GameRunning);
                    break;
                case "GAME_PAUSED":
                    SwitchState(GameStateType.GamePaused);
                    break;
                }*/
            }
            
            // if eventype = inputevent send information to Activestate.HandleKeyEvent and 
            // GameRunning.HandleKeyEvent to check if relevant and process inputEvent further. 
            else if (eventType == GameEventType.InputEvent) {
                ActiveState.HandleKeyEvent(gameEvent.Message, gameEvent.Parameter1);
                Console.WriteLine(gameEvent.Message);
            }
        }
    }
}